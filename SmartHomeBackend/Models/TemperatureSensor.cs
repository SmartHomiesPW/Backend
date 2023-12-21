using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SmartHomeBackend.Models
{
    [Table("Temperature_Sensors")]
    public class TemperatureSensor
    {
        [Key]
        public string Sensor_Id { get; set; }
        public string System_Id { get; set; }
        [ForeignKey("System_Id")]
        public string Name { get; set; }
        public string Details { get; set; }
        public decimal Value { get; set; }
    }
}
