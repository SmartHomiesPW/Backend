using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeBackendAcceptanceTests.Models
{
    [Table("Switchable_Lights")]
    public class SwitchableLight
    {
        [Key]
        public string Switchable_Light_Id { get; set; }
        [ForeignKey("System")]
        public string System_Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public int Value { get; set; }

    }
}
