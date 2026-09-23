using Clinic.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Clinic.API.Features.Posts.DeletePost;

public static class DeletePostHandler
{
    public static async Task<DeletePostResponse> Handle(
        DeletePostCommand command,
        AppDbContext db,
        CancellationToken ct)
    {
        var clinicPost = await db.ClinicPosts
            .FirstOrDefaultAsync(p => p.Id == command.Id, ct);

        if (clinicPost is null)
        {
            return new DeletePostResponse(false);
        }

        db.ClinicPosts.Remove(clinicPost);
        await db.SaveChangesAsync(ct);

        return new DeletePostResponse(true);
    }
}
