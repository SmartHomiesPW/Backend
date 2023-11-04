using System.ComponentModel.DataAnnotations;

namespace SmartHomeBackend.Models
{
    public class System
    {
        [Key]
        public string System_Id { get; set; }
        public string Name { get; set; }
    }
}
