namespace SmartHomeBackend.Models.Dto
{
    public class TemperatureSensorMeasureDto
    {
        public int sensorId { get; set; }
        public double temperature { get; set; }
        public string dateTime { get; set; }
    }
}
