using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeBackendAcceptanceTests.Models
{
    [Table("Alarms")]
    public class Alarm
    {
        [Key]
        public string Alarm_Id { get; set; }
        [ForeignKey("System")]
        public string System_Id { get; set; }
        public int IsActive { get; set; }
        public int IsTriggered { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string AccessCode { get; set; }
    }
}
