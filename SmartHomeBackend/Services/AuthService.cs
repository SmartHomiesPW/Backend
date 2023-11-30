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

    }
}
