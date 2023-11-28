using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHomeBackend.Services;
using System.Text.Json;

namespace SmartHomeBackend.Controllers.Devices
{
    [Route("api/system/{systemId}/board/{boardId}/devices/door-lock")]
    [ApiController]
    public class DoorLockController : ControllerBase
    {
        private readonly DeviceService _deviceService;

        public DoorLockController(DeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDoorLockProperties(int systemId, int boardId)
        {
            string url = $"http://127.0.0.1:5000/api/system/{systemId}/board/{boardId}/devices/door-lock";
            var (response, jsonDocument) = await _deviceService.SendHttpRequest(url);
            if (response.IsSuccessStatusCode)
            {
                return Ok(jsonDocument);
            }
            else
            {
                return StatusCode(int.Parse(response.StatusCode.ToString()), $"An error occurred: {response.Content}");
            }
        }

        [Route("log")]
        [HttpGet]
        public async Task<IActionResult> GetDoorLockState(int systemId, int boardId)
        {
            string url = $"http://127.0.0.1:5000/api/system/{systemId}/board/{boardId}/devices/door-lock/log";
            var (response, jsonDocument) = await _deviceService.SendHttpRequest(url);
            if (response.IsSuccessStatusCode)
            {
                return Ok(jsonDocument);
            }
            else
            {
                return StatusCode(int.Parse(response.StatusCode.ToString()), $"An error occurred: {response.Content}");
            }
        }
    }
}
