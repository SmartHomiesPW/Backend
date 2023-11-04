using System.ComponentModel.DataAnnotations;

namespace SmartHomeBackend.Models
{
    public class User
    {
        [Key]
        public string User_Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
