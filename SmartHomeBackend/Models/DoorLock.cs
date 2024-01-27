using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SmartHomeBackend.Models
{
    [Table("DoorLocks")]
    public class DoorLock
    {
        [Key]
        public string DoorLock_Id { get; set; } = "Unknown";
        [ForeignKey("System")]
        public string System_Id { get; set; } = "Unknown";
        public string Name { get; set; } = "Unknown";
        public string Details { get; set; } = "-";
        public int IsOn { get; set; }
    }
}
