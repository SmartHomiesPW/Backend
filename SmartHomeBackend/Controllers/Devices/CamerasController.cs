using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHomeBackend.Services;
using System.Text.Json;

namespace SmartHomeBackend.Controllers.Devices
{
    [Route("api/system/{systemId}/board/{boardId}/devices/cameras")]
    [ApiController]
    public class CamerasController : ControllerBase
    {
        private readonly DeviceService _deviceService;

        public CamerasController(DeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [Route("{cameraId}")]
        [HttpGet]
        public async Task<IActionResult> GetCameraProperties(int systemId, int boardId, int cameraId)
        {
            string url = $"http://127.0.0.1:5000/api/system/{systemId}/board/{boardId}/devices/cameras/{cameraId}";
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

        [Route("{cameraId}/log")]
        [HttpGet]
        public async Task<IActionResult> GetCameraRecordings(int systemId, int boardId, int cameraId)
        {
            string url = $"http://127.0.0.1:5000/api/system/{systemId}/board/{boardId}/devices/cameras/{cameraId}/log";
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
