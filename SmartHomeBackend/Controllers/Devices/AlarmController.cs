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
            string alarmId = alarmState.Alarm_Id;
            var alarmInDB = _context.Alarms.Find(alarmId);
            alarmInDB.IsActive = alarmState.IsActive;
            alarmInDB.IsTriggered = alarmState.IsTriggered;

            _context.SaveChanges();

            return Ok();

            //string url = $"{Strings.RPI_API_URL}/alarm";
            //var (response, _) = await _deviceService.SendHttpGetRequest(url);
            //if (response.IsSuccessStatusCode)
            //{
            //    return Ok(response);
            //}else
            //{
            //    return StatusCode(500, $"Error: {response}");
            //}
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

            return Ok();
        }

        [Route("data/{alarmId}")]
        [HttpGet]
        public async Task<IActionResult> GetAlarmData(string alarmId)
        {
            var alarmInDB = _context.Alarms.Find(alarmId);
            return Ok(alarmInDB);
        }

        [Route("log/{alarmId}")]
        [HttpGet]
        public async Task<IActionResult> GetAlarmLog(string alarmId)
        {
            var alarmsLogsInDB = _context.AlarmTriggers.Where(at => at.Alarm_Id.Equals(alarmId));
            return Ok(alarmsLogsInDB);
        }
    }
}
