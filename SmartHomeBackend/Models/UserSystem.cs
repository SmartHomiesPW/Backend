using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeBackend.Models
{
    [Table("UsersSystems")]
    public class UserSystem
    {
        [Key]
        public required Guid User_Id { get; init; }
        [ForeignKey("System")]
        public required string System_Id { get; set; }
    }
}
