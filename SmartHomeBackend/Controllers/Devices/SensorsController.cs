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

        [Route("humidity/states")]
        [HttpGet]
        public async Task<IActionResult> GetAllHumiditySensorsStates()
        {

            string url = $"{Strings.RPI_API_URL}/sensors/humidity";
            var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

            if (response.IsSuccessStatusCode)
            {
                var text = jsonDocument.RootElement.GetRawText();
                var array = JsonSerializer.Deserialize<HumiditySensorMeasureDto[]>(text);
                foreach(var sensor in array)
                {
                    var sensorInDB = _context.HumiditySensors.Find(sensor.SensorId);
                    sensorInDB.Value = (decimal)sensor.Humidity;
                }
                _context.SaveChanges();
            }
            
            return Ok(_context.HumiditySensors);
        }

        [Route("humidity/states/{sensorId}")]
        [HttpGet]
        public async Task<IActionResult> GetOneHumiditySensorState(int sensorId)
        {

            string url = $"{Strings.RPI_API_URL}/sensors/humidity";
            var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

            if (response.IsSuccessStatusCode)
            {
                var text = jsonDocument.RootElement.GetRawText();
                var array = JsonSerializer.Deserialize<HumiditySensorMeasureDto[]>(text);
                var sensor = array.Where(l => l.SensorId == sensorId).FirstOrDefault();
                var sensorInDB = _context.HumiditySensors.Find(sensorId);
                sensorInDB.Value = (decimal)sensor.Humidity;

                _context.SaveChanges();
            }
            
            return Ok(_context.HumiditySensors.Find(sensorId));
        }

        [Route("sunlight/states")]
        [HttpGet]
        public async Task<IActionResult> GetAllSunlightSensorsStates()
        {

            string url = $"{Strings.RPI_API_URL}/sensors/light";
            var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

            if (response.IsSuccessStatusCode)
            {
                var text = jsonDocument.RootElement.GetRawText();
                var array = JsonSerializer.Deserialize<SunlightSensorMeasureDto[]>(text);
                foreach (var sensor in array)
                {
                    var sensorInDB = _context.SunlightSensors.Find(sensor.SensorId);
                    sensorInDB.Value = (decimal)sensor.LightValue;
                }
                _context.SaveChanges();
            }

            return Ok(_context.SunlightSensors);
        }

        [Route("sunlight/states/{sensorId}")]
        [HttpGet]
        public async Task<IActionResult> GetOneSunlightSensorState(int sensorId)
        {

            string url = $"{Strings.RPI_API_URL}/sensors/light";
            var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

            if (response.IsSuccessStatusCode)
            {
                var text = jsonDocument.RootElement.GetRawText();
                var array = JsonSerializer.Deserialize<SunlightSensorMeasureDto[]>(text);
                var sensor = array.Where(l => l.SensorId == sensorId).FirstOrDefault();
                var sensorInDB = _context.SunlightSensors.Find(sensorId);
                sensorInDB.Value = (decimal)sensor.LightValue;

                _context.SaveChanges();
            }

            return Ok(_context.HumiditySensors.Find(sensorId));
        }

        [Route("temperature/states")]
        [HttpGet]
        public async Task<IActionResult> GetAlltemperatureSensorsStates()
        {

            string url = $"{Strings.RPI_API_URL}/sensors/temperature";
            var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

            if (response.IsSuccessStatusCode)
            {
                var text = jsonDocument.RootElement.GetRawText();
                var array = JsonSerializer.Deserialize<TemperatureSensorMeasureDto[]>(text);
                foreach (var sensor in array)
                {
                    var sensorInDB = _context.TemperatureSensors.Find(sensor.sensorId);
                    sensorInDB.Value = (decimal)sensor.temperature;
                }
                _context.SaveChanges();
            }

            return Ok(_context.SunlightSensors);
        }

        [Route("temperature/states/{sensorId}")]
        [HttpGet]
        public async Task<IActionResult> GetOneTemperatureSensorState(int sensorId)
        {

            string url = $"{Strings.RPI_API_URL}/sensors/temperature";
            var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

            if (response.IsSuccessStatusCode)
            {
                var text = jsonDocument.RootElement.GetRawText();
                var array = JsonSerializer.Deserialize<TemperatureSensorMeasureDto[]>(text);
                var sensor = array.Where(l => l.sensorId == sensorId).FirstOrDefault();
                var sensorInDB = _context.SunlightSensors.Find(sensorId);
                sensorInDB.Value = (decimal)sensor.temperature;

                _context.SaveChanges();
            }

            return Ok(_context.HumiditySensors.Find(sensorId));
        }

    }
}
