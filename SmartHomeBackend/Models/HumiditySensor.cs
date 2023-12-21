using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SmartHomeBackend.Models
{
    [Table("Humidity_Sensors")]
    public class HumiditySensor
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
