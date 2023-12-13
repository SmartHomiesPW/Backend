namespace SmartHomeBackend.Models.Dto
{
    public class SunlightSensorMeasureDto
    {
        public int Id { get; set; }
        public double LightValue { get; set; }
        public DateTime DateTime { get; set; }
        public int SensorId { get; set; }
    }
}
