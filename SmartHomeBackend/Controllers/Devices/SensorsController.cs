using Microsoft.AspNetCore.Mvc;
using SmartHomeBackend.Database;
using SmartHomeBackend.Models;
using SmartHomeBackend.Services;
using System.Text.Json;

namespace SmartHomeBackend.Controllers.Devices
{
    [Route("api/system/{systemId}/board/{boardId}/devices/sensors")]
    [ApiController]
    public class SensorsController : Controller
    {
        private readonly DeviceService _deviceService;
        private readonly SmartHomeDbContext _context;

        public SensorsController(SmartHomeDbContext context, DeviceService deviceService)
        {
            _deviceService = deviceService;
            _context = context;
        }

        [HttpGet("humidity")]
        public async Task<IActionResult> GetAllHumiditySensors(string boardId)
        {
            List<HumiditySensor> lightsOnBoard = _deviceService.GetAllHumiditySensors(boardId, _context);
            return Ok(lightsOnBoard);
        }

        [HttpGet("sunlight")]
        public async Task<IActionResult> GetAllSunlightSensors(string boardId)
        {
            List<SunlightSensor> lightsOnBoard = _deviceService.GetAllSunlightSensors(boardId, _context);
            return Ok(lightsOnBoard);
        }

        [HttpGet("temperature")]
        public async Task<IActionResult> GetAllTemperatureSensors(string boardId)
        {
            List<TemperatureSensor> lightsOnBoard = _deviceService.GetAllTemperatureSensors(boardId, _context);
            return Ok(lightsOnBoard);
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
