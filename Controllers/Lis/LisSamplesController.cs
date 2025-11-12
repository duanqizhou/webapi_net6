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
    [Route("api/lis/sample")]
    public class LisSamplesController : Controller
    {
        log4net.ILog log = log4net.LogManager.GetLogger(typeof(LisSamplesController));

        private readonly ISamApplyServices _samApplyServices;
        private readonly ISamUrgentvaluepromptServices _samUrgentvaluepromptServices;
        private readonly ISamSampleItemServices _samSampleItemServices;
        private readonly ISamSampleTypeServices _samSampleTypeServices;
        private readonly ISamTypeServices _samTypeServices;

        public LisSamplesController(
            ISamApplyServices samApplyServices, ISamUrgentvaluepromptServices samUrgentvaluepromptServices,
            ISamSampleItemServices samSampleItemServices, ISamSampleTypeServices samSampleTypeServices,
            ISamTypeServices samTypeServices)
        {
            _samApplyServices = samApplyServices;
            _samUrgentvaluepromptServices = samUrgentvaluepromptServices;
            _samSampleItemServices = samSampleItemServices;
            _samSampleTypeServices = samSampleTypeServices;
            _samTypeServices = samTypeServices;
        }

        [HttpGet("index")]
        public IActionResult Index()
        {
            return Ok();
        }
        [HttpGet("SamApply")]
        public async Task<IActionResult> SamApply()
        {
            try
            {
                var result = _samApplyServices.GetAll();
                return Ok(ApiResponse.Ok(result));
            }
            catch (Exception ex)
            {
                log.Error("获取样本信息失败", ex);
                return StatusCode(400, ApiResponse.Error("获取数据失败"));
            }
        }


        [HttpGet("SamUrgentvalueprompt")]
        public async Task<IActionResult> SamUrgentvalueprompt()
        {
            try
            {
                var result = _samUrgentvaluepromptServices.GetAll();
                return Ok(ApiResponse.Ok(result.Adapt<List<SamUrgentvaluepromptDto>>()));
            }
            catch (Exception ex)
            {
                log.Error("获取SamUrgentvalueprompt信息失败", ex);
                return StatusCode(400, ApiResponse.Error("获取数据失败"));
            }
        }

        [HttpPost("SamUrgentvaluepromptInsertOrUpdate")]
        public async Task<IActionResult> SamUrgentvaluepromptInsertOrUpdate(SamUrgentvaluepromptDto samUrgentvaluepromptDto)
        {
            try
            {
                var result = _samUrgentvaluepromptServices.InsertOrUpdate(samUrgentvaluepromptDto);
                return Ok(ApiResponse.Ok(result));
            }
            catch (Exception ex)
            {
                log.Error("SamUrgentvaluepromptInsertOrUpdate 信息失败", ex);
                return StatusCode(400, ApiResponse.Error("获取数据失败"));
            }
        }

        [HttpGet("SamSampleType")]
        public async Task<IActionResult> SamSampleType()
        {
            try
            {
                var result = _samSampleTypeServices.GetAll();
                return Ok(ApiResponse.Ok(result));
            }
            catch (Exception ex)
            {
                log.Error("获取 SamSampleType 信息失败", ex);
                return StatusCode(400, ApiResponse.Error("获取数据失败"));
            }

        }

        [HttpPost("SamType")]
        public async Task<IActionResult> SamType([FromBody] List<string> types = null)
        {
            try
            {
                var result = await _samTypeServices.GetListExpressionAsync(types);
                return Ok(ApiResponse.Ok(result));
            }
            catch (Exception ex)
            {
                log.Error("获取 SamType 信息失败", ex);
                return StatusCode(400, ApiResponse.Error("获取数据失败"));
            }
        }
        [HttpGet("SamSampleItem")]
        public async Task<IActionResult> SamSampleItem([FromQuery] SamSampleItemPageDto dto)
        {
            try
            {
                var (list, totalCount) = await _samSampleItemServices.GetSamSampleItemPageAsyncTotal(dto);
                return Ok(ApiResponse.Ok(new
                {
                    total = totalCount,
                    list
                }));
            }
            catch (Exception ex)
            {
                log.Error("获取 SamSampleItem 信息失败", ex);
                return StatusCode(400, ApiResponse.Error("获取数据失败"));
            }

        }
    }
}
