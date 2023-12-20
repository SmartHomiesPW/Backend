namespace SmartHomeBackend.Models.Dto
{
    public class TemperatureSensorMeasureDto
    {
        public DateTime dateTime { get; set; }
        public int sensorId { get; set; }
        public double temperature { get; set; }
    }
}
