namespace SmartHomeBackend.Models.Dto
{
    public class AlarmSensorStateDto
    {
        public string alarmSensorId {  get; set; }
        public string alarmId { get; set; }
        public int isOn { get; set; }
        public int movementDetected {  get; set; }
    }
}
