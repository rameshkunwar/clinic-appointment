using Clinic.API.Data;
using Clinic.API.Features.Auth.Models;
using Clinic.API.Features.Auth.Services;
using Microsoft.EntityFrameworkCore;

namespace Clinic.API.Features.Auth.Register
{
    public static class RegisterHandler
    {
        public static async Task<RegisterResponse> Handle(
            RegisterCommand command,
            AppDbContext db,
            IPasswordHasher passwordHasher,
            CancellationToken ct)
        {
            var existingUser = await db.StaffUsers.FirstOrDefaultAsync(u => u.Username == command.Username, ct);
            if (existingUser != null)
            {
                return new RegisterResponse(false, "Username already exists.");
            }

            // Prevent creation of another Admin if one already exists
            if (command.Role == Role.Admin)
            {
                var adminExists = await db.StaffUsers.AnyAsync(u => u.Role == Role.Admin, ct);
                if (adminExists)
                {
                    return new RegisterResponse(false, "An Admin user already exists. Cannot register another Admin.");
                }
            }

            var salt = Guid.NewGuid().ToString(); // Simple salt for now, can be improved
            var passwordHash = passwordHasher.HashPassword(command.Password + salt);

            var newUser = new StaffUser
            {
                Id = Guid.NewGuid(),
                Username = command.Username,
                PasswordHash = passwordHash,
                Salt = salt,
                Role = command.Role,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            db.StaffUsers.Add(newUser);
            await db.SaveChangesAsync(ct);

            return new RegisterResponse(true, "Registration successful.");
        }
    }

    public record RegisterResponse(bool Success, string Message);
}
