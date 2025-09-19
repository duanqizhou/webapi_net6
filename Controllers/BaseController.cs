using log4net;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using webapi.Common;
using webapi.Models.BaseData;
using webapi.Dtos;
using webapi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webapi.Controllers
{
    [Route("api/base")]
    [ApiController]
    public class BaseController : ControllerBase
    {

        private readonly ICacheService _cacheService;
        private readonly IBaseDrugServices _baseDrugServices;
        private readonly IBaseCureServices _baseCureServices;
        private readonly IBaseMatServices _baseMatServices;

        private readonly ILog _logger = LogManager.GetLogger(typeof(UserController));
        public BaseController(ICacheService cacheService, IBaseDrugServices baseDrugServices, IBaseCureServices baseCureServices, IBaseMatServices baseMatServices)
        {
            _cacheService = cacheService;
            _baseDrugServices = baseDrugServices;
            _baseCureServices = baseCureServices;
            _baseMatServices = baseMatServices;
        }

        #region 药品
        [HttpGet("tablesDicts")]
        public async Task<IActionResult> GetTablesDicts([FromQuery] DictLibListDto dto)
        {

            var (list, totalCount) = await _baseDrugServices.GetDM_DICTAsyncTotal(dto);

            return Ok(ApiResponse.Ok(new
            {
                total = totalCount,
                list = list
            }));
        }

        [HttpPut("tablesDicts")]
        public async Task<IActionResult> UpdateDicts([FromBody] DictLibDto dto)
        {
            try
            {
                var dict = _baseDrugServices.GetById(dto.CODE);
                if (dict == null)
                    return NotFound(ApiResponse.Fail("药品不存在"));
                dto.Adapt(dict);
                var result = await _baseDrugServices.UpdateAsync(dict);
                return result ? Ok(ApiResponse.Ok("修改成功")) : Ok(ApiResponse.Error("修改失败"));
            }
            catch (Exception ex)
            {
                return Ok(ApiResponse.Error("异常：" + ex.Message));
            }
        }

        [HttpPost("tablesDicts")]
        public async Task<IActionResult> AddDicts([FromBody] DictLibDto dto)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(dto.CODE))
                {
                    var dict = _baseDrugServices.GetById(dto.CODE);
                    if (dict != null)
                        return Ok(ApiResponse.Fail("药品编码已存在"));
                }
                //检查dto
                if (string.IsNullOrWhiteSpace(dto.CNAME))
                {
                    return Ok(ApiResponse.Fail("药品名称不能为空"));
                }
                int Code = await _baseDrugServices.GetMaxCode();
                string Type = dto.ClassifyCode == "11" ? "1" : "2";
                string Dose = "XX";
                string Action = "AA";
                string code = Type.Substring(0, 1) + Dose.Substring(0, 1) + Action.Substring(0, 1) + Code.ToString("D7");
                var entity = dto.Adapt<DM_DICT>();
                entity.CODE = code;
                var result = await _baseDrugServices.AddAsync(entity);
                return result ? Ok(ApiResponse.Ok("添加成功")) : Ok(ApiResponse.Error("添加失败"));
            }
            catch (Exception ex)
            {
                return Ok(ApiResponse.Error("异常：" + ex.Message));
            }
        }

        #endregion
        #region 治疗
        [HttpGet("tablesCures")]
        public async Task<IActionResult> GetTablesCures([FromQuery] CureLibListDto dto)
        {
            var (list, totalCount) = await _baseCureServices.GetCureAsyncTotal(dto);
            return Ok(ApiResponse.Ok(new
            {
                total = totalCount,
                list = list
            }));
        }

        [HttpPut("tablesCures")]
        public async Task<IActionResult> UpdateCures([FromBody] CureLibDto dto)
        {
            try
            {
                var cure = _baseCureServices.GetById(dto.ID);
                if (cure == null)
                    return NotFound(ApiResponse.Fail("项目不存在"));
                dto.Adapt(cure);
                var result = await _baseCureServices.UpdateAsync(cure);
                return result ? Ok(ApiResponse.Ok("修改成功")) : Ok(ApiResponse.Error("修改失败"));
            }
            catch (Exception ex)
            {
                return Ok(ApiResponse.Error("异常：" + ex.Message));
            }
        }
        #endregion
        #region 耗材、材料
        [HttpGet("tablesMats")]
        public async Task<IActionResult> GetTablesMats([FromQuery] MatLibListDto dto)
        {
            var (list, totalCount) = await _baseMatServices.GetMatAsyncTotal(dto);
            return Ok(ApiResponse.Ok(new
            {
                total = totalCount,
                list = list
            })); 
        }

        [HttpPut("tablesMats")]
        public async Task<IActionResult> UpdateMats([FromBody] MatLibDto dto)
        {
            try
            {
                var Mat = _baseMatServices.GetById(dto.ID);
                if (Mat == null)
                    return NotFound(ApiResponse.Fail("项目不存在"));
                dto.Adapt(Mat);
                var result = await _baseMatServices.UpdateAsync(Mat);
                return result ? Ok(ApiResponse.Ok("修改成功")) : Ok(ApiResponse.Error("修改失败"));
            }
            catch (Exception ex)
            {
                return Ok(ApiResponse.Error("异常：" + ex.Message));
            }
        }
        #endregion

    }
}
