using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeBackendAcceptanceTests.Models
{
    [Table("AlarmTriggers")]
    public class AlarmTrigger
    {
        [Key]
        public string AlarmTrigger_Id { get; set; }
        [ForeignKey("Alarm")]
        public string Alarm_Id { get; set; }
        public DateTime DateTime { get; set; }
    }
}
