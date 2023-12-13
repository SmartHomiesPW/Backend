namespace SmartHomeBackend.Models.Dto
{
    public class TemperatureSensorMeasureDto
    {
        public int Id { get; set; }
        public double Temperature { get; set; }
        public DateTime DateTime { get; set; }
        public int SensorId { get; set; }
    }
}
