using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHomeBackend.Services;
using System.Text.Json;
using SmartHomeBackend.Globals;
using SmartHomeBackend.Database;
using SmartHomeBackend.Models.Dto;

namespace SmartHomeBackend.Controllers.Devices
{
    [Route("api/system/1/board/1/devices/alarm")]
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
        public async Task<IActionResult> SetAlarmStateRPi([FromBody] AlarmStateDto alarmState)
        {
            string alarmId = alarmState.Alarm_Id;
            var alarmInDB = _context.Alarms.Find(alarmId);
            alarmInDB.IsActive = alarmState.IsActive;
            alarmInDB.IsTriggered = alarmState.IsTriggered;

            _context.SaveChanges();

            return Ok(alarmInDB);
        }

        /// <summary>
        /// Let's mobile app transfer the information about one of system alarms state's change to the cloud and to Raspberry Pi.
        /// </summary>
        /// <returns>Alarm's state in the database after operation on success.</returns>
        [Route("state")]
        [HttpPut]
        public async Task<IActionResult> SetAlarmState([FromBody] AlarmStateDto alarmState)
        {
            // Call do rpi zmieniający stan całego modułu alarmu

            string alarmId = alarmState.Alarm_Id;
            var alarmInDB = _context.Alarms.Find(alarmId);
            alarmInDB.IsActive = alarmState.IsActive;
            alarmInDB.IsTriggered = alarmState.IsTriggered;

            _context.SaveChanges();

            return Ok(alarmInDB);
        }

        /// <summary>
        /// Enables change of alarm's name, details and access code in the database.
        /// </summary>
        /// <returns>Alarm's state in the database after operation on success.</returns>
        [Route("properties")]
        [HttpPut]
        public async Task<IActionResult> SetAlarmProperties([FromBody] AlarmPropertiesDto alarmProperties)
        {
            string alarmId = alarmProperties.Alarm_Id;
            var alarmInDB = _context.Alarms.Find(alarmId);
            alarmInDB.Name = alarmProperties.Name;
            alarmInDB.Details = alarmProperties.Details;
            alarmInDB.AccessCode = alarmProperties.AccessCode;

            _context.SaveChanges();

            return Ok(alarmInDB);
        }

        /// <returns>Alarm's state in the database on success.</returns>
        [Route("{alarmId}")]
        [HttpGet]
        public async Task<IActionResult> GetAlarmFullInfo(string alarmId)
        {
            // Call do rpi pozyskujący aktualne dane o alarmie
            // Modyfikacja danych alarmu w bazie danych

            var alarmInDB = _context.Alarms.Find(alarmId);
            return Ok(alarmInDB);
        }

        /// <returns>Full information about all alarm's sensors on success.</returns>
        [Route("{alarmId}/sensors")]
        [HttpGet]
        public async Task<IActionResult> GetAlarmSensorsStates(string alarmId)
        {
            string url = $"{Strings.RPI_API_URL}/alarm/get";
            try
            {
                var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

                if (response.IsSuccessStatusCode)
                {
                    var text = jsonDocument.RootElement.GetRawText();
                    var array = JsonSerializer.Deserialize<AlarmSensorStateDtoBoardGet[]>(text);
                    foreach (var alarmSensor in array)
                    {
                        var alarmSensorInDB = _context.AlarmSensors.Where(x => x.Alarm_Id.Equals(alarmId) && x.Alarm_Sensor_Id.Equals(alarmSensor.alarmSensorId)).FirstOrDefault();
                        alarmSensorInDB.Is_On = alarmSensor.isOn;
                    }
                    _context.SaveChanges();
                }

                return Ok(_context.AlarmSensors);
            }
            catch
            {
                return Ok(_context.AlarmSensors);
            }
        }

        /// <summary>
        /// Enables change of alarm's sensor state in the database and in Raspberry Pi
        /// </summary>
        /// <returns>Alarm's sensor's state in the database after operation on success.</returns>
        [Route("{alarmId}/sensors")]
        [HttpPut]
        public async Task<IActionResult> SetAlarmSensorState(string alarmId, [FromBody] AlarmSensorStateDto alarmSensor)
        {
            string url = $"{Strings.RPI_API_URL}/alarm/set/{alarmSensor.alarmSensorId}/{alarmSensor.isOn}";

            try
            {
                var alarmSensorInDB = _context.AlarmSensors.Where(x => x.Alarm_Id.Equals(alarmId) && 
                                        x.Alarm_Sensor_Id.Equals(alarmSensor.alarmSensorId)).FirstOrDefault();
                if (alarmSensorInDB != null)
                {
                    var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

                    if (response.IsSuccessStatusCode)
                    {
                        alarmSensorInDB.Is_On = alarmSensor.isOn;

                        alarmSensorInDB.Movement_Detected = alarmSensor.isOn == 0 ? 0 : alarmSensor.movementDetected;

                        _context.SaveChanges();
                    }
                }
                return Ok(_context.AlarmSensors);
            }
            catch
            {
                return Ok(_context.AlarmSensors);
            }

        }

        /// <summary>
        /// Enables change of alarm's sensor state in the database from Raspberry Pi's request
        /// </summary>
        /// <returns>Alarm's sensor's state in the database after operation on success.</returns>
        [Route("sensorsRPi")]
        [HttpPut]
        public async Task<IActionResult> SetAlarmSensorStateRPi([FromBody] AlarmSensorStateDtoBoardTrigger alarmSensor)
        {
            // Call do rpi zmieniający stan czujnika alarmu

            var alarmSensorInDB = _context.AlarmSensors.Where(x => x.Alarm_Id.Equals(alarmSensor.alarmId) &&
                                        x.Alarm_Sensor_Id.Equals(alarmSensor.alarmSensorId)).FirstOrDefault();
            if (alarmSensorInDB != null)
            {
                alarmSensorInDB.Movement_Detected = 1;
                _context.SaveChanges();
            }

            return Ok(alarmSensorInDB);
        }

        [Route("{alarmId}/log")]
        [HttpGet]
        public async Task<IActionResult> GetAlarmLog(string alarmId)
        {
            var alarmsLogsInDB = _context.AlarmTriggers.Where(at => at.Alarm_Id.Equals(alarmId));
            return Ok(alarmsLogsInDB);
        }
    }
}
