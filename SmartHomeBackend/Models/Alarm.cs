using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeBackend.Models
{
    [Table("Alarms")]
    public class Alarm
    {
        [Key]
        public string Alarm_Id { get; set; } = "Unknown";
        [ForeignKey("System")]
        public string System_Id { get; set; } = "Unknown";
        public int IsActive { get; set; }
        public int IsTriggered { get; set; }
        public string Name { get; set; } = "Unknown";
        public string Details { get; set; } = "-";
        public string AccessCode { get; set; } = "Unknown";
    }
}
