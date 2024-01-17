using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeBackendAcceptanceTests.Models
{
    [Table("Humidity_Sensors_Logs")]
    public class HumiditySensorLog
    {
        [Key]
        public int Log_Id { get; set; }
        public double Humidity { get; set; }
        public DateTime DateTime { get; set; }
        [ForeignKey("HumiditySensor")]
        public int SensorId { get; set; }
    }
}
