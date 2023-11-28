using Microsoft.AspNetCore.Identity;
using SmartHomeBackend.Database;
using SmartHomeBackend.Models;

namespace SmartHomeBackend.Services
{
    public class AuthService
    {
        private readonly UserManager<User> _userManager;

        public AuthService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> CreateNewUser(User model)
        {
            var user = new User { Email = model.Email, Password = model.Password };
            var result = await _userManager.CreateAsync(user, model.Password);

            return result;
        }

    }
}
