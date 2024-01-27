using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHomeBackend.Services;
using System.Text.Json;
using SmartHomeBackend.Globals;
using SmartHomeBackend.Database;
using SmartHomeBackend.Models.Dto;
using System;

namespace SmartHomeBackend.Controllers.Devices
{
    /// <summary>
    /// Controller responsible for managing requests associated with alarm.
    /// </summary>
    [Route("api/system/{systemId}/board/{boardId}/devices/alarm")]
    [ApiController]
    public class AlarmController : ControllerBase
    {
        private readonly DeviceService _deviceService;
        private readonly SmartHomeDbContext _context;

        public AlarmController(SmartHomeDbContext context, DeviceService deviceService)
        {
            _deviceService = deviceService;
            _context = context;
        }

        /// <summary>
        /// Let's Raspberry Pi transfer the information about one of it's alarms state's change to the cloud.
        /// </summary>
        /// <returns>Alarm's state in the database after operation on success.</returns>
        [Route("stateRPi")]
        [HttpPut]
        public ObjectResult SetAlarmStateRPi([FromBody] AlarmStateDto alarmState)
        {
            try
            {
                if (_context.Alarms.Find(alarmState.Alarm_Id) == null)
                    throw new BadHttpRequestException($"Alarm with id {alarmState.Alarm_Id} not found in database.");

                string alarmId = alarmState.Alarm_Id;
                var alarmInDB = _context.Alarms.Find(alarmId);
                if (alarmInDB != null)
                {
                    alarmInDB.IsActive = alarmState.IsActive;
                    alarmInDB.IsTriggered = alarmState.IsTriggered;
                    _context.SaveChanges();
                }
                return Ok(alarmInDB);

            } catch (BadHttpRequestException ex) 
            {
                return StatusCode(400, ex.Message);
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Let's mobile app transfer the information about one of system alarms state's change to the cloud and to Raspberry Pi.
        /// </summary>
        /// <returns>Alarm's state in the database after operation on success.</returns>
        [Route("state")]
        [HttpPut]
        public async Task<ObjectResult> SetAlarmState([FromBody] AlarmStateDto alarmState)
        {
            try
            {
                var alarmInDB = _context.Alarms.Find(alarmState.Alarm_Id) ??
                    throw new BadHttpRequestException($"Alarm with id {alarmState.Alarm_Id} not found in database.");

                alarmInDB.IsActive = 1 - alarmState.IsActive;

                foreach(var alarmSensorInDB in _context.AlarmSensors)
                {
                    string url = $"{Strings.RPI_API_URL_MICHAL}/alarm/set/{alarmSensorInDB.Alarm_Sensor_Id}/{alarmSensorInDB.Is_On}";
                    var (response, _) = await _deviceService.SendHttpGetRequest(url);

                    if (response.IsSuccessStatusCode)
                    {
                        alarmSensorInDB.Is_On = alarmInDB.IsActive;
                        alarmSensorInDB.Movement_Detected = alarmSensorInDB.Is_On == 0 ? 0 : alarmSensorInDB.Movement_Detected;
                    }
                }

                alarmInDB.IsTriggered = alarmState.IsTriggered;
                _context.SaveChanges();

                return Ok(alarmInDB);
            }
            catch (BadHttpRequestException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Enables change of alarm's name, details and access code in the database.
        /// </summary>
        /// <returns>Alarm's state in the database after operation on success.</returns>
        [Route("properties")]
        [HttpPut]
        public ObjectResult SetAlarmProperties([FromBody] AlarmPropertiesDto alarmProperties)
        {
            try {

                var alarmInDB = _context.Alarms.Find(alarmProperties.Alarm_Id) ??
                    throw new BadHttpRequestException($"Alarm with id {alarmProperties.Alarm_Id} not found in database.");

                alarmInDB.Name = alarmProperties.Name;
                alarmInDB.Details = alarmProperties.Details;
                alarmInDB.AccessCode = alarmProperties.AccessCode;
                _context.SaveChanges();

                return Ok(alarmInDB);
            }
            catch (BadHttpRequestException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <returns>Alarm's state in the database on success.</returns>
        [Route("{alarmId}")]
        [HttpGet]
        public ObjectResult GetAlarmFullInfo(string alarmId)
        {
            try
            {
                var alarmInDB = _context.Alarms.Find(alarmId) ??
                    throw new BadHttpRequestException($"Alarm with id {alarmId} not found in database.");

                return Ok(alarmInDB);
            }
            catch (BadHttpRequestException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <returns>Full information about all alarm's sensors on success.</returns>
        [Route("{alarmId}/sensors")]
        [HttpGet]
        public async Task<ObjectResult> GetAlarmSensorsStates(string alarmId)
        {
            string url = $"{Strings.RPI_API_URL_MICHAL}/alarm/get";
            try
            {
                if (_context.Alarms.Find(alarmId) == null)
                    throw new BadHttpRequestException($"Alarm with id {alarmId} not found in database.");

                var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

                if (response.IsSuccessStatusCode)
                {
                    var text = jsonDocument.RootElement.GetRawText();
                    var array = JsonSerializer.Deserialize<AlarmSensorStateDtoBoardGet[]>(text) ?? 
                        throw new Exception("Couldn't deserialize response into AlarmSensorStateDtoBoardGet[].");
                    foreach (var alarmSensor in array)
                    {
                        var alarmSensorInDB = _context.AlarmSensors.Where(x => x.Alarm_Id.Equals(alarmId) && 
                            x.Alarm_Sensor_Id.Equals(alarmSensor.alarmSensorId)).FirstOrDefault();
                        if(alarmSensorInDB != null)
                            alarmSensorInDB.Is_On = alarmSensor.isOn;
                    }
                    _context.SaveChanges();
                }

                return Ok(_context.AlarmSensors);
            }
            catch (BadHttpRequestException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Enables change of alarm's sensor state in the database and in Raspberry Pi.
        /// </summary>
        /// <returns>Alarm's sensor's state in the database after operation on success.</returns>
        [Route("{alarmId}/sensors")]
        [HttpPut]
        public async Task<ObjectResult> SetAlarmSensorState(string alarmId, [FromBody] AlarmSensorStateDto alarmSensor)
        {
            string url = $"{Strings.RPI_API_URL_MICHAL}/alarm/set/{alarmSensor.alarmSensorId}/{alarmSensor.isOn}";

            try
            {
                if (_context.Alarms.Find(alarmId) == null)
                    throw new Exception($"Alarm with id {alarmId} not found in database.");
                if (_context.AlarmSensors.Find(alarmSensor.alarmSensorId) == null)
                    throw new Exception($"Alarm Sensor with id {alarmSensor.alarmSensorId} not found in database.");

                var alarmSensorInDB = _context.AlarmSensors.Where(x => x.Alarm_Id.Equals(alarmId) && 
                                        x.Alarm_Sensor_Id.Equals(alarmSensor.alarmSensorId)).FirstOrDefault();
                if (alarmSensorInDB != null)
                {
                    var (response, _) = await _deviceService.SendHttpGetRequest(url);

                    if (response.IsSuccessStatusCode)
                    {
                        alarmSensorInDB.Is_On = alarmSensor.isOn;

                        alarmSensorInDB.Movement_Detected = alarmSensor.isOn == 0 ? 0 : alarmSensor.movementDetected;

                        _context.SaveChanges();
                    }
                }
                return Ok(_context.AlarmSensors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Enables change of alarm's sensor state in the database from Raspberry Pi's request.
        /// </summary>
        /// <returns>Alarm's sensor's state in the database after operation on success.</returns>
        [Route("sensorsRPi")]
        [HttpPut]
        public ObjectResult SetAlarmSensorStateRPi([FromBody] AlarmSensorStateDtoBoardTrigger alarmSensor)
        {
            try {
                if (_context.Alarms.Find(alarmSensor.alarmId) == null)
                    throw new Exception($"Alarm with id {alarmSensor.alarmId} not found in database.");
                if (_context.AlarmSensors.Find(alarmSensor.alarmSensorId) == null)
                    throw new Exception($"Alarm Sensor with id {alarmSensor.alarmSensorId} not found in database.");

                var alarmSensorInDB = _context.AlarmSensors.Where(x => x.Alarm_Id.Equals(alarmSensor.alarmId) &&
                                            x.Alarm_Sensor_Id.Equals(alarmSensor.alarmSensorId)).FirstOrDefault();
                if (alarmSensorInDB != null)
                {
                    alarmSensorInDB.Movement_Detected = 1;
                    _context.SaveChanges();
                }

                return Ok(alarmSensorInDB);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
