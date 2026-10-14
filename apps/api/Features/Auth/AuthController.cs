using Microsoft.AspNetCore.Mvc;
using Wolverine;
using Clinic.API.Features.Auth.Register;
using Clinic.API.Features.Auth.Login;
using Clinic.API.Features.Auth.Logout;
using Microsoft.AspNetCore.Authorization;

namespace Clinic.API.Features.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMessageBus _messageBus;

        public AuthController(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("register")]
        public async Task<ActionResult<RegisterResponse>> Register([FromBody] RegisterCommand command)
        {
            var response = await _messageBus.InvokeAsync<RegisterResponse>(command);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginCommand command)
        {
            var response = await _messageBus.InvokeAsync<LoginResponse>(command);
            if (!response.Success)
            {
                return Unauthorized(response);
            }

            // Set the JWT as an HttpOnly cookie
            Response.Cookies.Append("X-Access-Token", response.Token ?? string.Empty,
                new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddHours(1), // Token expiry
                    IsEssential = true, // Mark the cookie as essential
                    SameSite = SameSiteMode.Strict, // CSRF protection
                    Secure = true // Only send over HTTPS
                });

            return Ok(new { response.Success, response.Message });
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult<LogoutResponse>> Logout()
        {
            // Clear the HttpOnly cookie
            Response.Cookies.Delete("X-Access-Token");

            var response = await _messageBus.InvokeAsync<LogoutResponse>(new LogoutCommand());
            return Ok(response);
        }
    }
}
