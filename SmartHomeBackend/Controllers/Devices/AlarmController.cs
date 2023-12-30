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

        [Route("stateRPI")]
        [HttpPut]
        public async Task<IActionResult> SetAlarmStateRPI([FromBody] AlarmStateDto alarmState)
        {
            string alarmId = alarmState.Alarm_Id;
            var alarmInDB = _context.Alarms.Find(alarmId);
            alarmInDB.IsActive = alarmState.IsActive;
            alarmInDB.IsTriggered = alarmState.IsTriggered;

            _context.SaveChanges();

            return Ok();
        }

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

        [Route("{alarmId}")]
        [HttpGet]
        public async Task<IActionResult> GetAlarmFullInfo(string alarmId)
        {
            var alarmInDB = _context.Alarms.Find(alarmId);
            return Ok(alarmInDB);
        }

        [Route("{alarmId}/sensors")]
        [HttpGet]
        public async Task<IActionResult> GetAlarmSensors(string alarmId)
        {
            // Call do rpi pozyskujący stany czujników alarmu

            var alarmSensorsInDB = _context.AlarmSensors.Where(x => x.Alarm_Id.Equals(alarmId));
            return Ok(alarmSensorsInDB);
        }

        [Route("{alarmId}/sensors")]
        [HttpPut]
        public async Task<IActionResult> SetAlarmSensorState(string alarmId, [FromBody] AlarmSensorStateDto alarmSensor)
        {
            // Call do rpi zmieniający stan czujnika alarmu

            var alarmSensorInDB = _context.AlarmSensors.Where(x => x.Alarm_Id.Equals(alarmId) && 
                                        x.Alarm_Sensor_Id.Equals(alarmSensor.alarmSensorId)).FirstOrDefault();
            if (alarmSensorInDB != null)
            {
                alarmSensorInDB.Is_On = alarmSensor.isOn;
                alarmSensorInDB.Movement_Detected = alarmSensor.movementDetected;
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
