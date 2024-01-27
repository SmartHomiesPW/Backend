using Microsoft.AspNetCore.Mvc;
using SmartHomeBackend.Database;
using SmartHomeBackend.Models.Dto;
using SmartHomeBackend.Services;

namespace SmartHomeBackend.Controllers.Auth
{
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

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto model)
        {
            try
            {
                var user = await _authService.CreateNewUser(model);

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

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginDto model)
        {
            try
            {
                var user = await _authService.FindUserFromLogin(model);

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
