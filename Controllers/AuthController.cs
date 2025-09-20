using Microsoft.AspNetCore.Mvc;
using webapi.Common;
using webapi.Models.BaseData;
using webapi.Dtos;
using webapi.Services;
using Mapster;
using Microsoft.Extensions.Options;
using webapi.Configs;
using Dm.util;
using Microsoft.AspNetCore.Authorization;
using log4net;
using System.Text;
using Dm.filter;
using webapi.Services.Lis;

namespace webapi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthController : ControllerBase
{
    private readonly string _lisDbName;
    private readonly JwtHelper _jwt;
    private readonly IAuthServices _services;
    private readonly JwtSettings _JWTsettings;
    private readonly IUserServices _userServices;
    private readonly IWwfPersonServices _wwfPersonServices;

    private readonly ILog log = LogManager.GetLogger("AuthController");
    public AuthController(JwtHelper jwt, IAuthServices services, IOptions<JwtSettings> settings, IUserServices userServices, IWwfPersonServices wwfPersonServices, IConfiguration configuration)
    {
        _services = services;
        _jwt = jwt;
        _JWTsettings = settings.Value;
        _userServices = userServices;
        _wwfPersonServices = wwfPersonServices;
        _lisDbName = configuration["DbNames:Lis"];

    }

    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginUserDto req)
    {

        if (req.code != "zdq")
            return Unauthorized(ApiResponse.Error("验证码错误", 401));
        if (string.IsNullOrWhiteSpace(req.EMPID))
            return BadRequest(ApiResponse.Error("用户名不能为空", 400));

        switch (req.Sys)
        {
            case SysEnum.HIS:

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
                var userIdStr = user.EMPID;
                var accessToken = _jwt.GenerateToken(userIdStr, user.NAME);
                var refreshToken = _jwt.GenerateRefreshToken();

                // 保存 RefreshToken（你需改造 UserToken 表或另建新表）
                var userToken = new UserToken
                {
                    UserId = Convert.ToInt32(userId),
                    UserIdStr = userIdStr,
                    RefreshToken = refreshToken,
                    ExpireAt = DateTime.UtcNow.AddDays(7)
                };
                _services.Add(userToken); // 注意这里 _services 可能需调整为专门 token 的 service

                return Ok(ApiResponse.Ok(new
                {
                    token = accessToken,
                    refreshToken,
                }));
            case SysEnum.LIS:
                var lisPer = _wwfPersonServices.GetById(req.EMPID, _lisDbName);

                if (lisPer == null)
                    return Unauthorized(ApiResponse.Error("未找到用户", 401));
                string pwd = Password.StrToEncrypt("MD5", req.Password);
                if (lisPer.fpass != pwd)
                {
                    return Unauthorized(ApiResponse.Error("用户名或密码错误", 401));
                }
                var fperson_id = lisPer.fperson_id;
                var accessTokenLis = _jwt.GenerateToken(fperson_id, lisPer.fname);
                var refreshTokenLis = _jwt.GenerateRefreshToken();
                var lisUserToken = new UserToken
                {
                    UserIdStr = fperson_id,
                    RefreshToken = refreshTokenLis,
                    ExpireAt = DateTime.UtcNow.AddDays(7)
                };
                _services.Add(lisUserToken);
                return Ok(ApiResponse.Ok(new
                {
                    token = accessTokenLis,
                    refreshTokenLis,
                }));
            default:
                return Ok(ApiResponse.OkMsg("无此系统权限", new { token = "" }));
        }

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


