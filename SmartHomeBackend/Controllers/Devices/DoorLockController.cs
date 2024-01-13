using Azure;
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

        [Route("states")]
        [HttpGet]
        public async Task<IActionResult> GetAllDoorLocksStates()
        {
            return Ok(_context.DoorLocks);
        }

        [HttpPut]
        [Route("set/{isOn}")]
        public async Task<IActionResult> SetDoorLockState(int isOn)
        {
            try
            {
                string url;
                if (isOn == 1)
                {
                    url = $"{Strings.RPI_API_URL}/door-lock/set/1";
                    _context.DoorLocks.ElementAt(0).IsOn = 1;
                }
                else
                {
                    url = $"{Strings.RPI_API_URL}/door-lock/set/0";
                    _context.DoorLocks.ElementAt(0).IsOn = 0;
                }
                var (response, _) = await _deviceService.SendHttpGetRequest(url);
                if (response.IsSuccessStatusCode)
                {
                    _context.SaveChanges();
                    return Ok("Success!");
                } else
                {
                    return Ok("Something went wrong...");
                }
            }
            catch
            {
                return Ok("Something went wrong...");
            }
        }
    }
}
