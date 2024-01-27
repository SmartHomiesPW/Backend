using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SmartHomeBackend.Models
{
    [Table("Temperature_Sensors")]
    public class TemperatureSensor
    {
        [Key]
        public string Sensor_Id { get; set; } = "Unknown";
        [ForeignKey("System")]
        public string System_Id { get; set; } = "Unknown";
        public string Name { get; set; } = "Unknown";
        public string Details { get; set; } = "-";
        public decimal Value { get; set; }
    }
}
