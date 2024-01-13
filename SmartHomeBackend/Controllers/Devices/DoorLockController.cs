using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHomeBackend.Database;
using SmartHomeBackend.Globals;
using SmartHomeBackend.Models.Dto;
using SmartHomeBackend.Services;
using System.Text.Json;

namespace SmartHomeBackend.Controllers.Devices
{
    [Route("api/system/{systemId}/board/{boardId}/devices/door-lock")]
    [ApiController]
    public class DoorLockController : ControllerBase
    {
        private readonly SmartHomeDbContext _context;
        private readonly DeviceService _deviceService;

        public DoorLockController(DeviceService deviceService, SmartHomeDbContext context)
        {
            _deviceService = deviceService;
            _context = context;
        }

        [HttpPut]
        [Route("set/{isOn}")]
        public async Task<IActionResult> SetDoorLockState(int isOn)
        {
            string url = "";
            if (isOn == 1)
            {
                url = $"{Strings.RPI_API_URL}/door-lock/set/1";
            }
            else
            {
                url = $"{Strings.RPI_API_URL}/door-lock/set/0";
            }
            try
            {
                var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

                return Ok("Success!");
            }
            catch
            {
                return Ok("Something went wrong...");
            }
        }
    }
}
