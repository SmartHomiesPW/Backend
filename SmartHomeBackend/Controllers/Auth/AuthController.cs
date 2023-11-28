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
        private readonly UserManager<User> _userManager;
        private readonly AuthService _authService;

        public AuthController(UserManager<User> userManager, AuthService authService)
        {
            _userManager = userManager;
            _authService = authService;
        }

        [Route("register")]
        [HttpGet]
        public async Task<IActionResult> RegisterUser([FromBody] User model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.CreateNewUser(model);

            if (result.Succeeded)
            {
                return Ok("User registered successfully");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByNameAsync(model.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return Unauthorized("Invalid username or password");
            }

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("key"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "issuer",
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds
                );

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }

            return Unauthorized("Invalid username or password");
        }
    }
}
