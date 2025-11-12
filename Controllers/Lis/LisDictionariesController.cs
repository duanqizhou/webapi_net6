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

namespace webapi.Controllers.Lis
{
    [ApiController]
    [Route("api/lis/dictionaries")]
    public class LisDictionariesController : ControllerBase
    {
        log4net.ILog log = log4net.LogManager.GetLogger(typeof(LisDictionariesController));
        private readonly IDictionaryService _dictionaryService;
        public LisDictionariesController(IDictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }
        [HttpGet("sjxm")]
        public async Task<IActionResult> Sjxm()
        {
            try
            {
                var result = await _dictionaryService.GetSjxmDropdownsAsync();
                return Ok(ApiResponse.Ok(result));
            }
            catch (Exception ex)
            {
                log.Error("获取字典数据失败", ex);
                return StatusCode(400, ApiResponse.Error("获取字典数据失败"));
            }
        }
    }
}
