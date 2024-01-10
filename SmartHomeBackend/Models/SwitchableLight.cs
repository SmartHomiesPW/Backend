using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeBackend.Models
{
    [Table("Switchable_Lights")]
    public class SwitchableLight
    {
        [Key]
        public string Switchable_Light_Id { get; set; }
        [ForeignKey("Board")]
        public string Board_Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public int Value { get; set; }

    }
}
