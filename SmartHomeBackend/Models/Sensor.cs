using System.ComponentModel.DataAnnotations;

namespace SmartHomeBackend.Models
{
    public class Sensor
    {
        [Key]
        public string Sensor_Id { get; set; }
    }
}
