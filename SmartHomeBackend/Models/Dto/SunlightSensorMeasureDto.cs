namespace SmartHomeBackend.Models.Dto
{
    public class SunlightSensorMeasureDto
    {
        public int sensorId { get; set; }
        public double lightValue { get; set; }
        public string dateTime { get; set; }
    }
}
