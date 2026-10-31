using System.Security.Claims;
using Clinic.API.Features.Auth.Models;
using Clinic.API.Entities;

namespace Clinic.API.Features.Auth.Services
{
    public interface ITokenService
    {
        AuthResponse GenerateJwtToken(User user);
    }
}
