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

        /// <returns>Information about current states of all humidity sensors connected to specific board within specific system on success.</returns>
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
                    var array = JsonSerializer.Deserialize<HumiditySensorMeasureDto[]>(text) ??
                        throw new Exception("Couldn't deserialize response into HumiditySensorMeasureDto[].");
                    foreach (var sensor in array)
                    {
                        var sensorInDB = _context.HumiditySensors.Find(sensor.sensorId.ToString()) ??
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
        /// <returns>Information about a current state of a specific humidity sensor connected to specific board within specific system on success.</returns>
        [Route("humidity/states/{sensorId}")]
        [HttpGet]
        public async Task<IActionResult> GetOneHumiditySensorState(int sensorId)
        {
            try
            {
                var sensorInDB = _context.HumiditySensors.Find(sensorId.ToString()) ??
                    throw new Exception($"Humidity Sensor with id {sensorId} not found in database.");

                string url = $"{Strings.RPI_API_URL_ADRIAN}/sensors/humidity";
                var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

                if (response.IsSuccessStatusCode)
                {
                    var text = jsonDocument.RootElement.GetRawText();
                    var array = JsonSerializer.Deserialize<HumiditySensorMeasureDto[]>(text) ??
                        throw new Exception("Couldn't deserialize response into HumiditySensorMeasureDto[].");
                    var sensor = array.First(l => l.sensorId == sensorId) ??
                        throw new Exception("Humidity Sensor's id from response is invalid.");
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

        /// <returns>Information about current states of all sunlight sensors connected to specific board within specific system on success.</returns>
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
                    var array = JsonSerializer.Deserialize<SunlightSensorMeasureDto[]>(text) ??
                        throw new Exception("Couldn't deserialize response into SunlightSensorMeasureDto[].");
                    foreach (var sensor in array)
                    {
                        var sensorInDB = _context.SunlightSensors.Find(sensor.sensorId.ToString()) ??
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

        /// <returns>Information about current states of a specific sunlight sensor connected to specific board within specific system on success.</returns>
        [Route("sunlight/states/{sensorId}")]
        [HttpGet]
        public async Task<IActionResult> GetOneSunlightSensorState(int sensorId)
        {
            try
            {
                var sensorInDB = _context.SunlightSensors.Find(sensorId.ToString()) ??
                    throw new Exception($"Temperature Sensor with id {sensorId} not found in database.");

                string url = $"{Strings.RPI_API_URL_ADRIAN}/sensors/light";
                var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

                if (response.IsSuccessStatusCode)
                {
                    var text = jsonDocument.RootElement.GetRawText();
                    var array = JsonSerializer.Deserialize<SunlightSensorMeasureDto[]>(text) ??
                        throw new Exception("Couldn't deserialize response into SunlightSensorMeasureDto[].");
                    var sensor = array.First(l => l.sensorId == sensorId) ??
                        throw new Exception("Sunlight Sensor's id from response is invalid.");
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

        /// <returns>Information about current states of all temperature sensors connected to specific board within specific system on success.</returns>
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
                    var array = JsonSerializer.Deserialize<TemperatureSensorMeasureDto[]>(text) ??
                        throw new Exception("Couldn't deserialize response into TemperatureSensorMeasureDto[].");
                    foreach (var sensor in array)
                    {
                        var sensorInDB = _context.TemperatureSensors.Find(sensor.sensorId.ToString()) ??
                            throw new Exception($"Temperature Sensor's with id {sensor.sensorId} not found in database.");
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

        /// <returns>Information about current state of a specific temperature sensor connected to specific board within specific system on success.</returns>
        [Route("temperature/states/{sensorId}")]
        [HttpGet]
        public async Task<IActionResult> GetOneTemperatureSensorState(int sensorId)
        {
            try
            {
                var sensorInDB = _context.TemperatureSensors.Find(sensorId.ToString()) ??
                    throw new Exception($"Temperature Sensor with id {sensorId} not found in database.");

                string url = $"{Strings.RPI_API_URL_ADRIAN}/sensors/temperature";
                var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

                if (response.IsSuccessStatusCode)
                {
                    var text = jsonDocument.RootElement.GetRawText();
                    var array = JsonSerializer.Deserialize<TemperatureSensorMeasureDto[]>(text) ??
                        throw new Exception("Couldn't deserialize response into TemperatureSensorMeasureDto[].");
                    var sensor = array.First(l => l.sensorId == sensorId) ??
                        throw new Exception("Temperature Sensor id from response is invalid.");
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
