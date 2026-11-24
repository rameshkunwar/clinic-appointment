namespace Clinic.API.Features.Auth.GetMe;

public record GetMeResponse(
    Guid Id,
    string Username,
    string Email
);