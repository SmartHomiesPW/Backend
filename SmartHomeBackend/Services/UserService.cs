using SmartHomeBackend.Database;
using SmartHomeBackend.Models;

namespace SmartHomeBackend.Services
{
    /// <summary>
    /// Service responsible for operations associated with users in database.
    /// </summary>
    public class UserService
    {
        private readonly SmartHomeDbContext _context;

        public UserService(SmartHomeDbContext context)
        {
            _context = context;
        }

        /// <returns>Information about all users in the database on success.</returns>
        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        /// <returns>Information about a specific user in the database on success.</returns>
        public User GetUser(string id)
        {
            return _context.Users.Where(u => u.User_Id.ToString().Equals(id)).First();
        }

        /// <returns>True on user existing in database and false on otherwise.</returns>
        public bool UserExists(string userId)
        {
            return _context.Users.Any(u => u.User_Id.ToString().Equals(userId));
        }
    }
}
