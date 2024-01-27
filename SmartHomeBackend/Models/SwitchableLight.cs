using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeBackend.Models
{
    [Table("Switchable_Lights")]
    public class SwitchableLight
    {
        [Key]
        public string Switchable_Light_Id { get; set; } = "Unknown";
        [ForeignKey("System")]
        public string System_Id { get; set; } = "Unknown";
        public string Name { get; set; } = "Unknown";
        public string Details { get; set; } = "-";
        public int Value { get; set; }

    }
}
