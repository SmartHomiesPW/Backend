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
            try
            {
                List<User> users = _userService.GetAllUsers();
                return Ok(users);
            } catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("{userId}")]
        [HttpGet]
        public async Task<IActionResult> GetUserWithId(string userId)
        {
            try
            {
                User user = _userService.GetUser(userId);
                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return StatusCode(400, $"An error occurred: User with given id doesn't exist.");
                }
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
