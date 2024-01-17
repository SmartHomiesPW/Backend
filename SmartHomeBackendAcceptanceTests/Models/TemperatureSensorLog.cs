using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeBackendAcceptanceTests.Models
{
    public class TemperatureSensorLog
    {
        [Key]
        public int Log_Id { get; set; }
        public double Temperature { get; set; }
        public DateTime DateTime { get; set; }
        [ForeignKey("TemperatureSensor")]
        public int SensorId { get; set; }
    }
}
