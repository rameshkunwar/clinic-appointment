using Clinic.API.Data;
using Clinic.API.Features.Auth.Services;
using Microsoft.EntityFrameworkCore;

namespace Clinic.API.Features.Auth.Login
{
    public static class LoginHandler
    {
        public static async Task<LoginResponse> Handle(
            LoginCommand command,
            AppDbContext db,
            IPasswordHasher passwordHasher,
            ITokenService tokenService,
            CancellationToken ct)
        {
            var user = await db.StaffUsers.FirstOrDefaultAsync(u => u.Username == command.Username, ct);
            if (user == null)
            {
                return new LoginResponse(false, "Invalid credentials.", null);
            }

            var isPasswordValid = passwordHasher.VerifyPassword(user.PasswordHash, command.Password + user.Salt);
            if (!isPasswordValid)
            {
                return new LoginResponse(false, "Invalid credentials.", null);
            }

            var token = tokenService.GenerateAccessToken(user);
            return new LoginResponse(true, "Login successful.", token);
        }
    }

    public record LoginResponse(bool Success, string Message, string? Token);
}
