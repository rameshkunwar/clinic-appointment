using System.Security.Claims;
using Clinic.API.Features.Auth.Models;

namespace Clinic.API.Features.Auth.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(StaffUser user);
        ClaimsPrincipal GetPrincipalFromToken(string token);
    }
}
