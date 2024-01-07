using Microsoft.AspNetCore.Mvc;
using SmartHomeBackend.Models.Dto;
using SmartHomeBackend.Services;

namespace SmartHomeBackend.Controllers.Auth
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto model)
        {
            var user = _authService.CreateNewUser(model);

            if (user != null)
            {
                return Ok(user);
            }

            return StatusCode(400, $"An error occurred: User with given mail already exists");
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginDto model)
        {
            var user = _authService.FindUserFromLogin(model);

            if (user != null)
            {
                return Ok(user);
            }

            return StatusCode(400, $"An error occurred: No user found");
        }
    }
}
