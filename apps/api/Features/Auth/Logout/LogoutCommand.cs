namespace Clinic.API.Features.Auth.Logout
{
    public record LogoutCommand();

    public record LogoutResponse(bool Success, string Message);
}
