using Clinic.API.Features.Auth.Models;

namespace Clinic.API.Features.Auth.Register
{
    public record RegisterCommand(string Username, string Password, Role Role);
}
