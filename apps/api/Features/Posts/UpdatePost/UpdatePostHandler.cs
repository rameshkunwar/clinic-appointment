using Clinic.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Clinic.API.Features.Posts.UpdatePost;

public static class UpdatePostHandler
{
    public static async Task<UpdatePostResponse?> Handle(
        UpdatePostCommand command,
        AppDbContext db,
        CancellationToken ct)
    {
        var clinicPost = await db.ClinicPosts
            .FirstOrDefaultAsync(p => p.Id == command.Id, ct);

        if (clinicPost is null)
        {
            return null;
        }

        clinicPost.Title = command.Title;
        clinicPost.Content = command.Content;
        clinicPost.Author = command.Author;
        clinicPost.UpdatedAt = DateTime.UtcNow;

        await db.SaveChangesAsync(ct);

        return new UpdatePostResponse(
            clinicPost.Id,
            clinicPost.Title,
            clinicPost.Content,
            clinicPost.Author,
            clinicPost.CreatedAt,
            clinicPost.UpdatedAt);
    }
}
