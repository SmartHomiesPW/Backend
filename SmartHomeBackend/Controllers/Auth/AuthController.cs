using Microsoft.AspNetCore.Mvc;
using SmartHomeBackend.Database;
using SmartHomeBackend.Models.Dto;
using SmartHomeBackend.Services;

namespace SmartHomeBackend.Controllers.Auth
{
    /// <summary>
    /// Controller responsible for managing requests associated with authentication.
    /// </summary>
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly SmartHomeDbContext _context;

        public AuthController(SmartHomeDbContext context, AuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        /// <summary>Registers a new user.</summary>
        /// <returns>New user's data in database on success.</returns>
        [Route("register")]
        [HttpPost]
        public ObjectResult RegisterUser([FromBody] UserRegistrationDto model)
        {
            try
            {
                var user = _authService.CreateNewUser(model);

                if (user != null)
                {
                    _context.UserSystems.Add(new Models.UserSystem() { User_Id = user.User_Id, System_Id = "1"});
                    _context.SaveChanges();
                    return Ok(user);
                }
                else
                {
                    return StatusCode(400, $"An error occurred: User with given mail already exists");
                }
            } catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>Logins a user.</summary>
        /// <returns>Logged in user's data in database on success.</returns>
        [Route("login")]
        [HttpPost]
        public ObjectResult Login([FromBody] UserLoginDto model)
        {
            try
            {
                var user = _authService.FindUserFromLogin(model);

                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return StatusCode(400, $"An error occurred: User credentials are invalid.");
                }
            } catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
