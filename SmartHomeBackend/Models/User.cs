using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeBackend.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public required Guid User_Id { get; init; }
        public required string Email { get; set; } = "Unknown";
        public required string Password { get; set; } = "Unknown";
        public string? FirstName { get; set; } = "Unknown";
        public string? LastName { get; set; } = "Unknown";
    }
}
