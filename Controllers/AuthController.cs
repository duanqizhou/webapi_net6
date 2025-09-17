using Microsoft.AspNetCore.Mvc;
using webapi.Common;
using webapi.Models;
using webapi.Services;
using Mapster;
using Microsoft.Extensions.Options;
using webapi.Configs;
using Dm.util;
using Microsoft.AspNetCore.Authorization;
using log4net;
using System.Text;
using Dm.filter;

namespace webapi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthController : ControllerBase
{
    private readonly JwtHelper _jwt;
    private readonly IAuthServices _services;
    private readonly JwtSettings _JWTsettings;
    private readonly IUserServices _userServices;
    private readonly ILog log = LogManager.GetLogger("AuthController");
    public AuthController(JwtHelper jwt, IAuthServices services, IOptions<JwtSettings> settings, IUserServices userServices)
    {
        _services = services;
        _jwt = jwt;
        _JWTsettings = settings.Value;
        _userServices = userServices;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginUserDto req)
    {
        if (req.code != "zdq")
            return Unauthorized(ApiResponse.Error("验证码错误", 401));

        if (string.IsNullOrWhiteSpace(req.EMPID) || string.IsNullOrWhiteSpace(req.Password))
            return BadRequest(ApiResponse.Error("用户名或密码不能为空", 400));

        // 获取用户，假设你用 EMPID 登录
        var user = _userServices.GetAll().FirstOrDefault(u => u.EMPID.ToLower() == req.EMPID.ToLower() || u.LOGINID.ToLower() == req.EMPID.ToLower());
        if (user == null)
            return Unauthorized(ApiResponse.Error("未找到用户", 401));

        if (!Password.Verify(req.Password, user.PASSWORD))
        {
            return Unauthorized(ApiResponse.Error("用户名或密码错误", 401));
        }

        // 创建 JWT Token
        var userId = user.EMPID;
        var accessToken = _jwt.GenerateToken(userId, user.NAME);
        var refreshToken = _jwt.GenerateRefreshToken();

        // 保存 RefreshToken（你需改造 UserToken 表或另建新表）
        var userToken = new UserToken
        {
            UserId = Convert.ToInt32(userId),
            RefreshToken = refreshToken,
            ExpireAt = DateTime.UtcNow.AddDays(7)
        };
        _services.Add(userToken); // 注意这里 _services 可能需调整为专门 token 的 service

        return Ok(ApiResponse.Ok(new
        {
            token = accessToken,
            refreshToken,
        }));
    }



    [HttpPost("refresh")]
    public IActionResult RefreshToken([FromBody] RefreshRequest req)
    {

        // 验证 refreshToken 是否存在
        var userToken = _services.GetAll().FirstOrDefault(t => t.RefreshToken == req.RefreshToken);
        if (userToken == null || userToken.ExpireAt < DateTime.UtcNow)
        {
            return Unauthorized(ApiResponse.Error("无效或过期的 Refresh Token", 401));
        }

        // 生成新的 access token
        var newAccessToken = _jwt.GenerateToken(userToken.UserId.ToString(), "admin");
        var newRefreshToken = _jwt.GenerateRefreshToken();

        // 更新数据库中的 refresh token
        userToken.RefreshToken = newRefreshToken;
        userToken.ExpireAt = DateTime.UtcNow.AddDays(7);
        _services.Update(userToken);
        var user = userToken.Adapt<UserTokenDto>();
        log.Info($"用户 {userToken.UserId} 刷新 Token 成功，生成新的 Access Token 和 Refresh Token");
        return Ok(ApiResponse.Ok(new
        {
            token = newAccessToken,
            refreshToken = newRefreshToken,
            user
        }));
    }

}


