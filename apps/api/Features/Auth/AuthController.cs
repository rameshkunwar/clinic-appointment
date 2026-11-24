using Microsoft.AspNetCore.Mvc;
using Wolverine;
using Clinic.API.Features.Auth.Register;
using Clinic.API.Features.Auth.Login;
using Clinic.API.Features.Auth.Logout;
using Clinic.API.Features.Auth.Models;
using Clinic.API.Features.Auth.GetMe;
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
        public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterRequest command)
        {
            var response = await _messageBus.InvokeAsync<AuthResponse>(command);
            if (response == null)
            {
                return BadRequest("Registration failed.");
            }
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest command)
        {
            var response = await _messageBus.InvokeAsync<AuthResponse>(command);
            if (response == null)
            {
                return Unauthorized("Login failed.");
            }

            // Set the JWT as an HttpOnly cookie
            Response.Cookies.Append("X-Access-Token", response.AccessToken ?? string.Empty,
                new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddHours(1), // Token expiry
                    IsEssential = true, // Mark the cookie as essential
                    SameSite = SameSiteMode.Strict, // CSRF protection
                    Secure = true // Only send over HTTPS
                });

            return Ok(response);
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<GetMeResponse>> GetMe([FromQuery] GetMeCommand command)
        {
            var response = await _messageBus.InvokeAsync<GetMeResponse>(command);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            // Clear the HttpOnly cookie
            Response.Cookies.Delete("X-Access-Token");

            await _messageBus.InvokeAsync<object>(new LogoutCommand());
            return Ok();
        }
    }
}