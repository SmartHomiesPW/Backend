using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SmartHomeBackend.Models;
using SmartHomeBackend.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

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
        public async Task<IActionResult> RegisterUser([FromBody] User model)
        {
            var user = _authService.CreateNewUser(model);
            return Ok(user.Result);
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] User model)
        {

            return Unauthorized("Invalid username or password");
        }
    }
}
