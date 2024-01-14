namespace SmartHomeBackend.Models.Dto
{
    public class AlarmSensorStateDtoBoardGet
    {
        public string alarmSensorId { get; set; }
        public string alarmId { get; set; }
        public int isOn { get; set; }
    }
}
