using Dm;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using webapi.Common;
using webapi.Dtos.His;
using webapi.Dtos.Lis;
using webapi.Models.LIS;
using webapi.Services;
using webapi.Services.Lis;

namespace webapi.Controllers.Lis
{
    [ApiController]
    [Route("api/lis")]
    public class LisController : ControllerBase
    {
        log4net.ILog log = log4net.LogManager.GetLogger(typeof(LisController));
        private readonly ISamItemServices _samItemServices;
        private readonly ISamInstrServices _samInstrServices;
        public LisController(ISamItemServices samItemServices, ISamInstrServices samInstrServices)
        {
            _samItemServices = samItemServices;
            _samInstrServices = samInstrServices;
        }
        [HttpGet("SamInstr")]
        public async Task<IActionResult> SamInstr()
        {
            try
            {
                var result = _samInstrServices.GetAll();
                return Ok(ApiResponse.Ok(result.Adapt<List<SamInstrDto>>()));
            }
            catch (Exception ex)
            {
                log.Error("获取仪器数据失败", ex);
                return StatusCode(400, ApiResponse.Error("获取数据失败"));
            }
        }
        [HttpGet("SamItem")]
        public async Task<IActionResult> SamItem([FromQuery] string inster)
        {
            try
            {
                var result = await _samItemServices.GetListExpressionAsync((w) => w.finstr_id == inster);
                return Ok(ApiResponse.Ok(result.Adapt<List<SamItemDto>>()));
            }
            catch (Exception ex)
            {
                log.Error("获取仪器项目数据失败", ex);
                return StatusCode(400, ApiResponse.Error("获取数据失败"));
            }
        }

    }
}