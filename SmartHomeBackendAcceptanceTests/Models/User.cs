using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeBackendAcceptanceTests.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public required Guid User_Id { get; init; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
