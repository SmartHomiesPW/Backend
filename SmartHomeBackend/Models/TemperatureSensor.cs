using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeBackend.Models
{
    [Table("Temperature_Sensors")]
    public class TemperatureSensor
    {
        [Key]
        public string Sensor_Id { get; set; }
        [ForeignKey("Board")]
        public string Board_Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public decimal Value { get; set; }
    }
}
