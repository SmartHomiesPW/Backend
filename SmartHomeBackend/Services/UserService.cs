using SmartHomeBackend.Database;
using SmartHomeBackend.Models;

namespace SmartHomeBackend.Services
{
    public class UserService
    {
        private readonly SmartHomeDbContext _context;

        public UserService(SmartHomeDbContext context)
        {
            _context = context;
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUser(string id)
        {
            return _context.Users.Where(u => u.User_Id.ToString().Equals(id)).First();
        }

        public bool UserExists(string userId)
        {
            return _context.Users.Any(u => u.User_Id.ToString().Equals(userId));
        }
    }
}
