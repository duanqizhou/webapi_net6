using Dm;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SqlSugar;
using System.Collections.Generic;
using System.Linq.Expressions;
using webapi.Common;
using webapi.Dtos.His;
using webapi.Dtos.Lis;
using webapi.Models.LIS;
using webapi.Services;
using webapi.Services.Lis;
using ICacheService = webapi.Common.ICacheService;

namespace webapi.Controllers.Lis
{
    [ApiController]
    [Route("api/lis/organizations")]
    public class LisOrganizationsController : Controller
    {
        log4net.ILog log = log4net.LogManager.GetLogger(typeof(LisOrganizationsController));

        private readonly IWwfFuncServices _wwfFuncServices;
        private readonly IWwfOrgServices _wwfOrgServices;

        public LisOrganizationsController(IWwfFuncServices wwfFuncServices, IWwfOrgServices wwfOrgServices)
        {
            _wwfFuncServices = wwfFuncServices;
            _wwfOrgServices = wwfOrgServices;
        }
        [HttpGet("index")]
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpGet("UserOrg/{personId}")]
        public async Task<IActionResult> GetOrgInfo(string personId)
        {
            try
            {
                var result = await _wwfOrgServices.GetOrgWithDeptAndPositionAsync(personId);
                return Ok(ApiResponse.Ok(result));
            }
            catch (Exception ex)
            {
                log.Error("获取机构信息失败", ex);
                return StatusCode(400, ApiResponse.Error("获取数据失败"));
            }

        }
        [HttpGet("SwitchSetting/{pid}")]
        public async Task<IActionResult> SwitchSetting(int pid)
        {
            try
            {
                var result = await _wwfFuncServices.GetMixSwitchSetAsync(pid);
                return Ok(ApiResponse.Ok(result));
            }
            catch (Exception ex)
            {
                log.Error("获取开关信息失败", ex);
                return StatusCode(400, ApiResponse.Error("获取数据失败"));
            }
        }

        [HttpPut("SwitchSetting")]
        public async Task<IActionResult> UpdateSwitchSetting(MixSwitchSetDto switchSetDto)
        {
            try
            {
                var result = await _wwfFuncServices.updateMixSwitchSetAsync(switchSetDto);
                return Ok(ApiResponse.Ok(result));
            }
            catch (Exception ex)
            {
                log.Error("获取开关信息失败", ex);
                return StatusCode(400, ApiResponse.Error("获取数据失败"));
            }
        }
    }
}
