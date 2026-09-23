using Clinic.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Clinic.API.Features.Posts.GetPost;

public static class GetPostHandler
{
    public static async Task<GetPostResponse?> Handle(
        GetPostQuery query,
        AppDbContext db,
        CancellationToken ct)
    {
        var clinicPost = await db.ClinicPosts
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == query.Id, ct);

        if (clinicPost is null)
        {
            return null;
        }

        return new GetPostResponse(
            clinicPost.Id,
            clinicPost.Title,
            clinicPost.Content,
            clinicPost.Author,
            clinicPost.CreatedAt,
            clinicPost.UpdatedAt);
    }
}
