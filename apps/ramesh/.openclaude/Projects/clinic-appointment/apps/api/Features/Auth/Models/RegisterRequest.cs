namespace Clinic.API.Features.Auth.Models;

public record RegisterRequest(
    string Email,
    string Password,
    string ConfirmPassword
);