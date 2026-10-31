namespace Clinic.API.Features.Auth.Models;

public record AuthResponse(
    string AccessToken,
    DateTime ExpiresAt
);