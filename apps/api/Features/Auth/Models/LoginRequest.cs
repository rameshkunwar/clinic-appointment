namespace Clinic.API.Features.Auth.Models;

public record LoginRequest(
    string Email,
    string Password
);