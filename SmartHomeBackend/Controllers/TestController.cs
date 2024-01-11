using Microsoft.AspNetCore.Mvc;
using SmartHomeBackend.Database;
using SmartHomeBackend.Services;

namespace SmartHomeBackend.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly DeviceService _deviceService;
        private readonly SmartHomeDbContext _context;

        [HttpGet]
        public async Task<IActionResult> TestFunction()
        {
            return Ok("test");
        }

        [HttpPost]
        public async Task<IActionResult> CreateTestSystemList()
        {


            return Ok("test");
        }
    }
}
