using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeBackend.Models
{
    [Table("Systems")]
    public class System
    {
        [Key]
        public string System_Id { get; set; } = "Unknown";
        public string Name { get; set; } = "Unknown";
    }
}
