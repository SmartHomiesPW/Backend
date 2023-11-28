using Microsoft.AspNetCore.Mvc;
using SmartHomeBackend.Services;
using System.Text.Json;

namespace SmartHomeBackend.Controllers.Devices
{
    [Route("api/system/{systemId}/board/{boardId}/devices/sensors")]
    [ApiController]
    public class SensorsController : Controller
    {
        private readonly DeviceService _deviceService;

        public SensorsController(DeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [Route("humidity/{sensorId}")]
        [HttpGet]
        public async Task<IActionResult> GetHumiditySensorProperties(int systemId, int boardId, int sensorId)
        {
            string url = $"http://127.0.0.1:5000/api/system/{systemId}/board/{boardId}/devices/sensors/humidity/{sensorId}";
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

        [Route("humidity/{sensorId}/log")]
        [HttpGet]
        public async Task<IActionResult> GetHumiditySensorMeasurement(int systemId, int boardId, int sensorId)
        {
            string url = $"http://127.0.0.1:5000/api/system/{systemId}/board/{boardId}/devices/sensors/humidity/{sensorId}/log";
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

        [Route("sunlight-intensity/{sensorId}")]
        [HttpGet]
        public async Task<IActionResult> GetSunlightIntensitySensorProperties(int systemId, int boardId, int sensorId)
        {
            string url = $"http://127.0.0.1:5000/api/system/{systemId}/board/{boardId}/devices/sensors/sunlight-intensity/{sensorId}";
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

        [Route("sunlight-intensity/{sensorId}/log")]
        [HttpGet]
        public async Task<IActionResult> GetSunlightIntensitySensorMeasurement(int systemId, int boardId, int sensorId)
        {
            string url = $"http://127.0.0.1:5000/api/system/{systemId}/board/{boardId}/devices/sensors/sunlight-intensity/{sensorId}/log";
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

        [Route("temperature/{sensorId}")]
        [HttpGet]
        public async Task<IActionResult> GetTemperatureSensorProperties(int systemId, int boardId, int sensorId)
        {
            string url = $"http://127.0.0.1:5000/api/system/{systemId}/board/{boardId}/devices/sensors/temperature/{sensorId}";
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

        [Route("temperature/{sensorId}/log")]
        [HttpGet]
        public async Task<IActionResult> GetTemperatureSensorMeasurement(int systemId, int boardId, int sensorId)
        {
            string url = $"http://127.0.0.1:5000/api/system/{systemId}/board/{boardId}/devices/sensors/temperature/{sensorId}/log";
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
