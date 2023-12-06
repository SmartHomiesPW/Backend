using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartHomeBackend.Database;
using SmartHomeBackend.Models;
using System.Text.Json;

namespace SmartHomeBackend.Services
{
    public class AuthService
    {
        private readonly SmartHomeDbContext _context;

        public AuthService(SmartHomeDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateNewUser(User model)
        {
            var user = new User { User_Id = model.User_Id, Email = model.Email, Password = model.Password };
            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public async Task<User> RemoveUser(User model)
        {
            var userToDelete = _context.Users.Where(u => u.User_Id.Equals(model.User_Id)).FirstOrDefault();

            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
                _context.SaveChanges();
            }

            return model;
        }

        public async Task<bool> VerifyUser(User model)
        {
            return _context.Users.Any(u => u.Email.Equals(model.Email) && u.Password.Equals(model.Password));
        }

    }
}
