using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using webapi.Common;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserRolesServices _userRolesServices;
        public UserController(IUserRolesServices userRolesServices)
        {
            _userRolesServices = userRolesServices;
        }

        [HttpGet("Me")]
        public async Task<IActionResult> GetMe()
        {
            // 从 JWT 中获取用户 ID
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var userNameClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

            if (userIdClaim == null || userNameClaim == null)
            {
                return Unauthorized(ApiResponse.Error("无法识别用户", 401));
            }

            int userId = int.Parse(userIdClaim.Value);
            string userName = Convert.ToString(userNameClaim.Value);


            // 获取角色名称列表
            var roleNames = await _userRolesServices.GetRoleNamesByUserIdAsync(userId);

            return Ok(ApiResponse.Ok(new
            {
                username = userName,
                roles = roleNames
            }));
        }

    }
}
