using Clinic.API.Data;
using Clinic.API.Entities;
using Clinic.API.Features.Auth.Models;
using Clinic.API.Features.Auth.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Clinic.API.Features.Auth.Register;

public record RegisterRequest(string Email, string Password, string ConfirmPassword);
public record RegisterResponse(AuthResponse AuthResponse, bool Success, string Message);

    public static class RegisterHandler
    {
        public static async Task<RegisterResponse> Handle(
            RegisterRequest command,
            AppDbContext db,
            UserService userService,
            ITokenService tokenService,
            CancellationToken ct)
        {
            var existingUser = await db.Users.SingleOrDefaultAsync(u => u.Email == command.Email, ct);
            if (existingUser != null)
            {
                throw new ValidationException("User with this email already exists.");
            }

            var user = await userService.CreateUser(command.Email, command.Password);
            var token = tokenService.GenerateJwtToken(user);

            return new RegisterResponse(token, true, "Registration successful.");
        }
}

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
        RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Passwords do not match.");
    }
}

