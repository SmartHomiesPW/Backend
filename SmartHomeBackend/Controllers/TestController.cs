using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHomeBackend.Models;
using SmartHomeBackend.Services;

namespace SmartHomeBackend.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public ObjectResult TestFunction()
        {
            return Ok("test");
        }
    }
}
