using Dm;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Diagnostics;
using System.Security.Claims;
using webapi.Common;
using webapi.Models.BaseData;
using webapi.Dtos;
using webapi.Repository;
using webapi.Services;
using ICacheService = webapi.Common.ICacheService;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly SqlSugarTransactionHelper _tranHelper;
        private readonly IUserServices _userServices;
        private readonly IEmployeeServices _employeeServices;

        private readonly ICacheService _cacheService;
        private readonly ILog _logger = LogManager.GetLogger(typeof(UserController));

        public UserController(IUserServices userServices, ICacheService cacheService, IEmployeeServices employeeServices, SqlSugarTransactionHelper tranHelper)
        {
            _userServices = userServices;
            _employeeServices = employeeServices;
            _cacheService = cacheService;
            _tranHelper = tranHelper;
        }

        [HttpGet("GetMaxEmpid")]
        public IActionResult Get()
        {
            try
            {
                var maxLoginId = _userServices.GetMaxLoginIdAsync().Result.ToString();
                return Ok(ApiResponse.Ok(maxLoginId));
            }
            catch (Exception ex)
            {
                // 记录异常日志
                _logger.Error($"Error in GetMaxEmpid: {ex.Message}");
                return StatusCode(500, ApiResponse.Error("获取最大登录ID失败"));
            }
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

            return Ok(ApiResponse.Ok(new
            {
                username = userName,
                roles = new List<string> { "ADMIN" }, // 这里可以替换成实际的角色列表
            }));
        }

        [HttpGet("TablesUsers")]
        public async Task<IActionResult> GetTablesUsers([FromQuery] UserQueryDto dto)
        {
            var currentUserId = UserContextHelper.GetCurrentUserId(User);
            if (currentUserId == null)
                return Unauthorized(ApiResponse.Error("无法识别用户", 401));

            var (userList, totalCount) = await _userServices.GetUsersAsyncTotal(dto);
            // 获取部门数据
            var departments = _cacheService.GetDepartments();
            // 获取员工数据
            var employees = _cacheService.GetEmployees();


            var result = userList.Select(u =>
            {
                var emp = employees.Find(ef => ef.EMPID == Convert.ToInt32(u.EMPID));

                u.DESCRIPTION = departments.Find(d => d.ID == emp?.DEPTID)?.NAME ?? "正在分配中";
                return new TableUserData
                {
                    loginid = u.LOGINID,
                    category = u.CATEGORY,
                    name = u.NAME,
                    attributes = u.ATTRIBUTES,
                    logincount = u.LOGINCOUNT.ToString(),
                    description = u.DESCRIPTION,
                    empid = u.EMPID,
                    doctorcode = emp?.DoctorCode ?? string.Empty
                };
            }).ToList();



            // 批量查角色
            //var userIds = result.Select(r => r.id).ToList();
            //var userRoles = await _userRolesServices.GetUserRoleWithNameByUserIdsAsync(userIds); // 你可以写一个服务扩展方法

            //var roleDict = userRoles
            //    .GroupBy(ur => ur.UserId)
            //    .ToDictionary(g => g.Key, g => g.Select(r => r.RoleName).ToList());

            //foreach (var r in result)
            //{
            //    if (roleDict.TryGetValue(r.id, out var roles))
            //        r.roles = string.Join(",", roles);
            //}

            return Ok(ApiResponse.Ok(new
            {
                total = totalCount,
                list = result
            }));
        }


        [HttpPut("TablesUsers")]
        public async Task<IActionResult> UpdateUser([FromBody] CreateOrUpdateUserDto dto)
        {

            try
            {
                var user = _userServices.GetSingle(s => s.LOGINID == dto.loginid);

                if (user == null)
                    return NotFound(ApiResponse.Fail("用户不存在"));
                var emp = _employeeServices.GetById(Convert.ToInt32(user.EMPID));
                if (emp == null)
                    return NotFound(ApiResponse.Fail("角色不存在"));

                if (!string.IsNullOrWhiteSpace(dto.password))
                {
                    user.PASSWORD = Password.Encrypt(dto.password);
                }
                emp.DoctorCode = dto.doctorcode;
                var result = await _tranHelper.ExecuteAsync(async db =>
                {
                    await db.Updateable(user).ExecuteCommandAsync();
                    await db.Updateable(emp).ExecuteCommandAsync();
                });
                _cacheService.RemoveCache(CacheName.employee);
                return Ok(ApiResponse.Ok(result));
            }
            catch (Exception ex)
            {
                return Ok(ApiResponse.Error("异常：" + ex.Message));
            }
        }

        [HttpPost("TablesUsers")]
        public async Task<IActionResult> AddUser([FromBody] CreateOrUpdateUserDto dto)
        {
            try
            {
                var maxLoginId = await _userServices.GetMaxLoginIdAsync();

                var user = new ACCOUNTS
                {
                    LOGINID = maxLoginId.ToString(),
                    PASSWORD = string.IsNullOrWhiteSpace(dto.password) ? Password.Encrypt("123456") : Password.Encrypt(dto.password),
                    CATEGORY = dto.category,
                    NAME = dto.name,
                    DESCRIPTION = dto.description,
                    EMPID = maxLoginId.ToString(),
                    OrgCode = 100010,
                    ATTRIBUTES = 1,
                    LOGINCOUNT = 0
                };
                var employee = new EMPLOYEE
                {
                    EMPID = maxLoginId,
                    LOGINID = maxLoginId.ToString(),
                    IDCODE = "",
                    DEPTID = int.TryParse(dto.description, out _) ? int.Parse(dto.description) : 0,
                    NAME = dto.name,
                    CONTRACTID = "",
                    RECORDID = "",
                    HEADSHIPID = 1,
                    ATTRIBUTE = 1,
                    TYPE = 1,
                    LEVEL = 1,
                    EDUCATION = 1,
                    NATIONALITY = 1,
                    STATE = 1,
                    NATIVEPLACE = 1,
                    OrgCode = 100010,
                    DoctorCode = dto.doctorcode
                };
                _cacheService.RemoveCache(CacheName.employee);
                var result = await _tranHelper.ExecuteAsync(async db =>
                {
                    await db.Insertable(user).ExecuteCommandAsync();
                    await db.Insertable(employee).ExecuteCommandAsync();
                });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ApiResponse.Error("异常：" + ex.Message));
            }
        }



        [HttpDelete("TablesUsers/{loginid}")]
        public async Task<IActionResult> DeleteUser(string loginid)
        {
            var user = _userServices.GetSingle(s => s.LOGINID == loginid);
            var emp = _employeeServices.GetById(Convert.ToInt32(user?.EMPID));
            var result = await _tranHelper.ExecuteAsync(async db =>
            {
                await db.Deleteable(user).ExecuteCommandAsync();
                await db.Deleteable(emp).ExecuteCommandAsync();
            });
            return Ok(result);
        }

        [HttpPut("TablesUsersStatus")]
        public async Task<IActionResult> UpdateUserStatus([FromBody] UpdateStatusUserDto ids)
        {
            var user = _userServices.GetSingle(s => s.LOGINID == ids.Loginid);
            if (user == null)
                return NotFound(ApiResponse.Fail("用户不存在"));
            user.ATTRIBUTES = ids.Status;
            var updated = _userServices.Update(user) > 0;
            return Ok(ApiResponse.Ok(updated));
        }
    }
}
