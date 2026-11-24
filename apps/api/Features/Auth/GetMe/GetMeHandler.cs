using Clinic.API.Data;
using Clinic.API.Features.Auth.GetMe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Clinic.API.Features.Auth.GetMe;

public static class GetMeHandler
{
    public static async Task<GetMeResponse> Handle(
        GetMeCommand command,
        AppDbContext db,
        ClaimsPrincipal user,
        CancellationToken ct)
    {
        var userIdStr = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
        {
            throw new UnauthorizedAccessException("User ID not found in claims.");
        }

        var staffUser = await db.StaffUsers
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync(ct);

        if (staffUser == null)
        {
            throw new UnauthorizedAccessException("User not found.");
        }

        return new GetMeResponse(
            staffUser.Id,
            staffUser.Username,
            staffUser.Email
        );
    }
}