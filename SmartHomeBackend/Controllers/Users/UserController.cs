using Microsoft.AspNetCore.Mvc;
using SmartHomeBackend.Models;
using SmartHomeBackend.Services;

namespace SmartHomeBackend.Controllers.Users
{
    /// <summary>
    /// Controller responsible for managing requests associated with users in database.
    /// </summary>
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        /// <returns>Information about all users from the database on success.</returns>
        [HttpGet]
        public ObjectResult GetAllUsers()
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

        /// <returns>Information about a specific user from the database on success.</returns>
        [Route("{userId}")]
        [HttpGet]
        public ObjectResult GetUserWithId(string userId)
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
