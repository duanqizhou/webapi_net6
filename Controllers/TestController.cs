using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.Common;
using webapi.Models.BaseData;
using webapi.Dtos;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        public TestController()
        {
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

    }
}
