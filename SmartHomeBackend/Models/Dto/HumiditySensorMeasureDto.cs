namespace SmartHomeBackend.Models.Dto
{
    public class HumiditySensorMeasureDto
    {
        public int sensorId { get; set; }
        public double humidity { get; set; }
        public string dateTime { get; set; }

    }
}
