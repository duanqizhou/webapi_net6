using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.Common;
using webapi.Models.BaseData;
using webapi.Dtos;
using webapi.Services.Lis;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IWwfPersonServices _wwfPersonServices;

        public TestController(IWwfPersonServices wwfPersonServices)
        {
            _wwfPersonServices = wwfPersonServices;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(ApiResponse.Ok("Test GetAll successful"));
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] UserDto entity)
        {
            return Ok(ApiResponse.Ok("Test Create successful"));
        }

        [AllowAnonymous]
        [HttpPost("Lislogin")]
        public IActionResult Lislogin([FromBody] WwfPersonDto testDto)
        {
            var aa = _wwfPersonServices.GetAll("LIS");
            var pas = Password.Encrypt(testDto.fpass);
            return Ok(ApiResponse.Ok("Test Create successful"));
        }
    }
}
