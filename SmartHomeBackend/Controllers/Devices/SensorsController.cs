using Microsoft.AspNetCore.Mvc;
using SmartHomeBackend.Database;
using SmartHomeBackend.Globals;
using SmartHomeBackend.Models;
using SmartHomeBackend.Models.Dto;
using SmartHomeBackend.Services;
using System.Text.Json;

namespace SmartHomeBackend.Controllers.Devices
{
    /// <summary>
    /// Controller responsible for managing requests associated with sensors.
    /// </summary>
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
            try
            {
                string url = $"{Strings.RPI_API_URL_ADRIAN}/sensors/humidity";
                var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

                if (response.IsSuccessStatusCode)
                {
                    var text = jsonDocument.RootElement.GetRawText();
                    var array = JsonSerializer.Deserialize<HumiditySensorMeasureDto[]>(text);
                    if (array == null)
                        throw new Exception("Couldn't deserialize response into HumiditySensorMeasureDto[].");
                    foreach (var sensor in array)
                    {
                        var sensorInDB = _context.HumiditySensors.Find(sensor.sensorId.ToString());
                        if (sensorInDB == null)
                            throw new Exception($"Humidity Sensor with id {sensor.sensorId} not found in database.");
                        sensorInDB.Value = (decimal)sensor.humidity;
                    }
                    _context.SaveChanges();
                    
                    return Ok(_context.HumiditySensors);
                } else
                {
                    throw new Exception("Couldn't get all humidity sensors states.");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("humidity/states/{sensorId}")]
        [HttpGet]
        public async Task<IActionResult> GetOneHumiditySensorState(int sensorId)
        {
            try
            {
                var sensorInDB = _context.HumiditySensors.Find(sensorId.ToString());
                if (sensorInDB == null)
                    throw new Exception($"Humidity Sensor with id {sensorId} not found in database.");

                string url = $"{Strings.RPI_API_URL_ADRIAN}/sensors/humidity";
                var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

                if (response.IsSuccessStatusCode)
                {
                    var text = jsonDocument.RootElement.GetRawText();
                    var array = JsonSerializer.Deserialize<HumiditySensorMeasureDto[]>(text);
                    if (array == null)
                        throw new Exception("Couldn't deserialize response into HumiditySensorMeasureDto[].");
                    var sensor = array.Where(l => l.sensorId == sensorId).FirstOrDefault();
                    if (sensor == null)
                        throw new Exception("Humidity Sensor id from response is invalid.");
                    sensorInDB.Value = (decimal)sensor.humidity;

                    _context.SaveChanges();
                    
                    return Ok(_context.HumiditySensors.Find(sensorId.ToString()));
                }
                else
                {
                    throw new Exception("Couldn't get a humidity sensor state.");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("sunlight/states")]
        [HttpGet]
        public async Task<IActionResult> GetAllSunlightSensorsStates()
        {
            try
            {
                string url = $"{Strings.RPI_API_URL_ADRIAN}/sensors/light";
                var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

                if (response.IsSuccessStatusCode)
                {
                    var text = jsonDocument.RootElement.GetRawText();
                    var array = JsonSerializer.Deserialize<SunlightSensorMeasureDto[]>(text);
                    if (array == null)
                        throw new Exception("Couldn't deserialize response into SunlightSensorMeasureDto[].");
                    foreach (var sensor in array)
                    {
                        var sensorInDB = _context.SunlightSensors.Find(sensor.sensorId.ToString());
                        if (sensorInDB == null)
                            throw new Exception($"Sunlight Sensor with id {sensor.sensorId} not found in database.");
                        sensorInDB.Value = (decimal)sensor.lightValue;
                    }
                    _context.SaveChanges();
                    
                    return Ok(_context.SunlightSensors);
                }
                else
                {
                    throw new Exception("Couldn't get all sunlight sensors states.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("sunlight/states/{sensorId}")]
        [HttpGet]
        public async Task<IActionResult> GetOneSunlightSensorState(int sensorId)
        {
            try
            {
                var sensorInDB = _context.SunlightSensors.Find(sensorId.ToString());
                if (sensorInDB == null)
                    throw new Exception($"Temperature Sensor with id {sensorId} not found in database.");

                string url = $"{Strings.RPI_API_URL_ADRIAN}/sensors/light";
                var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

                if (response.IsSuccessStatusCode)
                {
                    var text = jsonDocument.RootElement.GetRawText();
                    var array = JsonSerializer.Deserialize<SunlightSensorMeasureDto[]>(text);
                    if (array == null)
                        throw new Exception("Couldn't deserialize response into SunlightSensorMeasureDto[].");
                    var sensor = array.Where(l => l.sensorId == sensorId).FirstOrDefault();
                    if (sensor == null)
                        throw new Exception("Humidity Sensor id from response is invalid.");
                    sensorInDB.Value = (decimal)sensor.lightValue;

                    _context.SaveChanges();
                    
                    return Ok(_context.SunlightSensors.Find(sensorId.ToString()));
                }
                else
                {
                    throw new Exception("Couldn't get a sunlight sensor state.");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("temperature/states")]
        [HttpGet]
        public async Task<IActionResult> GetAllTemperatureSensorsStates()
        {
            try
            {
                string url = $"{Strings.RPI_API_URL_ADRIAN}/sensors/temperature";
                var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

                if (response.IsSuccessStatusCode)
                {
                    var text = jsonDocument.RootElement.GetRawText();
                    var array = JsonSerializer.Deserialize<TemperatureSensorMeasureDto[]>(text);
                    if (array == null)
                        throw new Exception("Couldn't deserialize response into TemperatureSensorMeasureDto[].");
                    foreach (var sensor in array)
                    {
                        var sensorInDB = _context.TemperatureSensors.Find(sensor.sensorId.ToString());
                        if (sensorInDB == null)
                            throw new Exception($"Temperature Sensor with id {sensor.sensorId} not found in database.");
                        sensorInDB.Value = (decimal)sensor.temperature;
                    }
                    _context.SaveChanges();
                    return Ok(_context.TemperatureSensors);
                } else
                {
                    throw new Exception("Couldn't get all temperature sensors states.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("temperature/states/{sensorId}")]
        [HttpGet]
        public async Task<IActionResult> GetOneTemperatureSensorState(int sensorId)
        {
            try
            {
                var sensorInDB = _context.TemperatureSensors.Find(sensorId.ToString());
                if (sensorInDB == null)
                    throw new Exception($"Temperature Sensor with id {sensorId} not found in database.");

                string url = $"{Strings.RPI_API_URL_ADRIAN}/sensors/temperature";
                var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

                if (response.IsSuccessStatusCode)
                {
                    var text = jsonDocument.RootElement.GetRawText();
                    var array = JsonSerializer.Deserialize<TemperatureSensorMeasureDto[]>(text);
                    if (array == null)
                        throw new Exception("Couldn't deserialize response into TemperatureSensorMeasureDto[].");
                    var sensor = array.Where(l => l.sensorId == sensorId).FirstOrDefault();
                    if (sensor == null)
                        throw new Exception("Humidity Sensor id from response is invalid.");
                    sensorInDB.Value = (decimal)sensor.temperature;

                    _context.SaveChanges();
                    
                    return Ok(_context.TemperatureSensors.Find(sensorId.ToString()));
                }
                else
                {
                    throw new Exception("Couldn't get a temperature sensor state.");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
