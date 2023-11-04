using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeBackend.Models
{
    [Table("Switchable_Lights")]
    public class SwitchableLight
    {
        [Key]
        public string Switchable_Light_Id { get; set; }
        public string System_Id { get; set; }
        [ForeignKey("System_Id")]
        public System System { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }

    }
}
