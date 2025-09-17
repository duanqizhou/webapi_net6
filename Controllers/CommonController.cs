using Microsoft.AspNetCore.Mvc;
using webapi.Common;
using webapi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webapi.Controllers
{
    [Route("api/common")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly ICacheService _cacheService;
        public CommonController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        [HttpGet("departments")]
        public async Task<IActionResult> GetDepartments()
        {
            return Ok(ApiResponse.Ok(_cacheService.GetDepartments()));
        }

        [HttpGet("employees")]
        public async Task<IActionResult> GetEmployees()
        {
            return Ok(ApiResponse.Ok(_cacheService.GetEmployees()));
        }

        [HttpGet("appsettings")]
        public async Task<IActionResult> GetAppsettings()
        {
            return Ok(ApiResponse.Ok(_cacheService.GetAppsettings()));
        }
    }
}
