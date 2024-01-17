using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SmartHomeBackendAcceptanceTests.Models
{
    [Table("AlarmSensors")]
    public class AlarmSensor
    {
        [Key]
        public string Alarm_Sensor_Id { get; set; }
        [ForeignKey("Alarm")]
        public string Alarm_Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public int Is_On { get; set; }
        public int Movement_Detected { get; set; }
    }
}
