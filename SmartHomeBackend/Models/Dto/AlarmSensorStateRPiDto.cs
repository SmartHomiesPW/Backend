namespace SmartHomeBackend.Models.Dto
{
    public class AlarmSensorStateRPiDto
    {
        public string alarmSensorId { get; set; }
        public string alarmId { get; set; }
        public int movementDetected { get; set; }
    }
}
