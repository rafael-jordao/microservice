using Microsoft.AspNetCore.Mvc;
using UserMicroservice.Models;
using UserMicroservice.Services;

namespace UserMicroservice.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly TokenService _tokenService;

        public AuthController(UserService userService, TokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            try
            {
                if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
                {
                    return BadRequest("Invalid request.");
                }

                var authenticatedUser = await _userService.AuthenticateUser(user.Email, user.Password);

                if (authenticatedUser == null)
                {
                    return Unauthorized("Invalid credentials.");
                }

                var token = _tokenService.GenerateJwtToken(authenticatedUser);

                return Ok(new
                {
                    authenticatedUser.Name,
                    authenticatedUser.Email,
                    token
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}, Verifique os campos");
            }
        }

    }
}
