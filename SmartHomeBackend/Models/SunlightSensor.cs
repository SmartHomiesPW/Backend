using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeBackend.Models
{
    [Table("Light_Sensors")]
    public class SunlightSensor
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
