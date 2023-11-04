using Microsoft.EntityFrameworkCore;
using SmartHomeBackend.Database;

namespace SmartHomeBackend.Services
{
    public class UserService
    {
        private readonly SmartHomeDbContext _context;

        public UserService(SmartHomeDbContext context)
        {
            _context = context;
        }

        public bool UserExists(string userId)
        {
            return _context.Users.Any(u => u.User_Id == userId);
        }
    }
}
