using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHomeBackend.Services;
using System.Text.Json;

namespace SmartHomeBackend.Controllers.Devices
{
    [Route("api/system/{systemId}/board/{boardId}/devices/alarm")]
    [ApiController]
    public class AlarmController : ControllerBase
    {
        private readonly DeviceService _deviceService;

        public AlarmController(DeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAlarmProperties(int systemId, int boardId)
        {
            string url = $"http://127.0.0.1:5000/api/system/{systemId}/board/{boardId}/devices/alarm";
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
        public async Task<IActionResult> GetAlarmState(int systemId, int boardId)
        {
            string url = $"http://127.0.0.1:5000/api/system/{systemId}/board/{boardId}/devices/alarm/log";
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
