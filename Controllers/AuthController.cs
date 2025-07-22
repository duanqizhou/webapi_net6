using Microsoft.AspNetCore.Mvc;
using webapi.Common;
using webapi.Models;
using webapi.Services;
using Mapster;
using Microsoft.Extensions.Options;
using webapi.Configs;
using Dm.util;
using Microsoft.AspNetCore.Authorization;

namespace webapi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthController : ControllerBase
{
    private readonly JwtHelper _jwt;
    private readonly IAuthServices _services;
    private readonly JwtSettings _JWTsettings;
    private readonly IUserServices _userServices;

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
        //string hash = BCrypt.Net.BCrypt.HashPassword("123456");
        if (req.code != "zdq")
        {
            return Unauthorized(ApiResponse.Error("验证码错误", 401));
        }
        if (req == null || string.IsNullOrEmpty(req.Username) || string.IsNullOrEmpty(req.Password))
        {
            return BadRequest(ApiResponse.Error("用户名或密码不能为空", 400));
        }
        // 验证用户名和密码
        var user = _userServices.GetAll().FirstOrDefault(u => u.Username == req.Username);
        if (user == null)
        {
            return Unauthorized(ApiResponse.Error("未找到用户", 401));
        }
        // 使用 BCrypt 验证密码
        // req.Password = "123456"
        // user.PasswordHash = "$2a$11$YsmKOlTYHDkSE2J2TOqUbOkIX38yHLv.lZ3LMs6TgXBqT1ti0qihW"

        bool passwordValid = BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash);
        if (!passwordValid)
        {
            return Unauthorized(ApiResponse.Error("用户名或密码错误", 401));
        }
        string key = _JWTsettings.SecretKey;
        var userId = user.Id;
        var accessToken = _jwt.GenerateToken(userId.ToString(), user.Username);
        var refreshToken = _jwt.GenerateRefreshToken();

        // 保存 refreshToken 到数据库（你也可以用 Redis）
        var userToken = new UserToken
        {
            UserId = userId,
            RefreshToken = refreshToken,
            ExpireAt = DateTime.UtcNow.AddDays(7) // 设置过期时间为 7 天
        };
        _services.Add(userToken);

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

        return Ok(ApiResponse.Ok(new
        {
            token = newAccessToken,
            refreshToken = newRefreshToken,
            user
        }));
    }

}


