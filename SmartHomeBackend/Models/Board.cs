using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeBackend.Models
{
    [Table("Boards")]
    public class Board
    {
        [Key]
        public string Board_Id { get; set; }
        [ForeignKey("System")]
        public string System_Id { get; set; }
        [Required]
        public string Board_URL { get; set; }
        public string Name { get; set; }
    }
}
