using Microsoft.AspNetCore.Mvc;
using SmartHomeBackend.Database;
using SmartHomeBackend.Globals;
using SmartHomeBackend.Models;
using SmartHomeBackend.Models.Dto;
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
            List<HumiditySensor> humiditySensorsOnBoard = _deviceService.GetAllHumiditySensors(boardId, _context);
            return Ok(humiditySensorsOnBoard);
        }

        [Route("humidity/states")]
        [HttpGet]
        public async Task<IActionResult> GetAllHumiditySensorsStates()
        {
            // rpi url = database.extractUrlBasedOnSystemIdAndBoardId

            string url = $"{Strings.RPI_API_URL}/sensors/humidity";
            var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

            if (response.IsSuccessStatusCode)
            {
                return Ok(jsonDocument);
            }
            else
            {
                return StatusCode(int.Parse(response.StatusCode.ToString()), $"An error occurred: {response.Content}");
            }
        }

        [Route("humidity/states/{sensorId}")]
        [HttpGet]
        public async Task<IActionResult> GetOneHumiditySensorState(int sensorId)
        {
            // rpi url = database.extractUrlBasedOnSystemIdAndBoardId

            string url = $"{Strings.RPI_API_URL}/sensors/humidity";
            var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

            if (response.IsSuccessStatusCode)
            {
                var text = jsonDocument.RootElement.GetRawText();
                var array = JsonSerializer.Deserialize<SunlightSensorMeasureDto[]>(text);
                var sensor = array.Where(l => l.SensorId == sensorId).FirstOrDefault();

                return Ok(sensor);
            }
            else
            {
                return StatusCode(int.Parse(response.StatusCode.ToString()), $"An error occurred: {response.Content}");
            }
        }

        [HttpGet("sunlight")]
        public async Task<IActionResult> GetAllSunlightSensors(string boardId)
        {
            List<SunlightSensor> sunlightSensorsOnBoard = _deviceService.GetAllSunlightSensors(boardId, _context);
            return Ok(sunlightSensorsOnBoard);
        }

        [Route("sunlight/states")]
        [HttpGet]
        public async Task<IActionResult> GetAllSunlightSensorsStates()
        {
            // rpi url = database.extractUrlBasedOnSystemIdAndBoardId

            string url = $"{Strings.RPI_API_URL}/sensors/light";
            var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

            if (response.IsSuccessStatusCode)
            {
                return Ok(jsonDocument);
            }
            else
            {
                return StatusCode(int.Parse(response.StatusCode.ToString()), $"An error occurred: {response.Content}");
            }
        }

        [Route("sunlight/states/{sensorId}")]
        [HttpGet]
        public async Task<IActionResult> GetOneSunlightSensorState(int sensorId)
        {
            // rpi url = database.extractUrlBasedOnSystemIdAndBoardId

            string url = $"{Strings.RPI_API_URL}/sensors/sunlight";
            var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

            if (response.IsSuccessStatusCode)
            {
                var text = jsonDocument.RootElement.GetRawText();
                var array = JsonSerializer.Deserialize<SunlightSensorMeasureDto[]>(text);
                var sensor = array.Where(l => l.SensorId == sensorId).FirstOrDefault();

                return Ok(sensor);
            }
            else
            {
                return StatusCode(int.Parse(response.StatusCode.ToString()), $"An error occurred: {response.Content}");
            }
        }

        [HttpGet("temperature")]
        public async Task<IActionResult> GetAllTemperatureSensors(string boardId)
        {
            List<TemperatureSensor> temperatureSensorsOnBoardBoard = _deviceService.GetAllTemperatureSensors(boardId, _context);
            return Ok(temperatureSensorsOnBoardBoard);
        }

        [Route("temperature/states")]
        [HttpGet]
        public async Task<IActionResult> GetAlltemperatureSensorsStates()
        {
            // rpi url = database.extractUrlBasedOnSystemIdAndBoardId

            string url = $"{Strings.RPI_API_URL}/sensors/temperature";
            var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

            if (response.IsSuccessStatusCode)
            {
                return Ok(jsonDocument);
            }
            else
            {
                return StatusCode(int.Parse(response.StatusCode.ToString()), $"An error occurred: {response.Content}");
            }
        }

        [Route("temperature/states/{sensorId}")]
        [HttpGet]
        public async Task<IActionResult> GetOneTemperatureSensorState(int sensorId)
        {
            // rpi url = database.extractUrlBasedOnSystemIdAndBoardId

            string url = $"{Strings.RPI_API_URL}/sensors/temperature";
            var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

            if (response.IsSuccessStatusCode)
            {
                var text = jsonDocument.RootElement.GetRawText();
                var array = JsonSerializer.Deserialize<TemperatureSensorMeasureDto[]>(text);
                var sensor = array.Where(l => l.SensorId == sensorId).FirstOrDefault();

                return Ok(sensor);
            }
            else
            {
                return StatusCode(int.Parse(response.StatusCode.ToString()), $"An error occurred: {response.Content}");
            }
        }

    }
}
