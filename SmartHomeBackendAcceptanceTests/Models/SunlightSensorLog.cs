using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeBackendAcceptanceTests.Models
{
    public class SunlightSensorLog
    {
        [Key]
        public int Log_Id { get; set; }
        public double LightValue { get; set; }
        public DateTime DateTime { get; set; }
        [ForeignKey("SunlightSensor")]
        public int SensorId { get; set; }
    }
}
