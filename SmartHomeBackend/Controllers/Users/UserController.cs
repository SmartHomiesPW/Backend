using Microsoft.AspNetCore.Mvc;
using SmartHomeBackend.Models;
using SmartHomeBackend.Services;

namespace SmartHomeBackend.Controllers.Users
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            List<User> users = _userService.GetAllUsers();
            return Ok(users);
        }

        [Route("{userId}")]
        [HttpGet]
        public async Task<IActionResult> GetUserWithId(string userId)
        {
            User user = _userService.GetUser(userId);
            return Ok(user);
        }
    }
}
