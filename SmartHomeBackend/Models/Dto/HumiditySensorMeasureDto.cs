namespace SmartHomeBackend.Models.Dto
{
    public class HumiditySensorMeasureDto
    {
        public int Id { get; set; }
        public double Humidity { get; set; }
        public DateTime DateTime { get; set; }
        public int SensorId { get; set; }

    }
}
