using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeBackend.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public string User_Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
