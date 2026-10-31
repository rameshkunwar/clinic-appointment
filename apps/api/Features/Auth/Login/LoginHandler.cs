using Clinic.API.Data;
using Clinic.API.Features.Auth.Models;
using Clinic.API.Features.Auth.Services;
using FluentValidation;

namespace Clinic.API.Features.Auth.Login;

public static class LoginHandler
{
    public static async Task<AuthResponse> Handle(
        LoginRequest command,
        UserService userService,
        ITokenService tokenService)
    {
        var user = await userService.ValidateUserCredentials(command.Email, command.Password);
        if (user == null)
        {
            throw new ValidationException("Invalid credentials.");
        }

        var token = tokenService.GenerateJwtToken(user);
        return token;
    }
}

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}

