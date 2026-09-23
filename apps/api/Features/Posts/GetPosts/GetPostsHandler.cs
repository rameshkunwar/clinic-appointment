using Clinic.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Clinic.API.Features.Posts.GetPosts;

public static class GetPostsHandler
{
    public static async Task<GetPostsResponse> Handle(
        GetPostsQuery query,
        AppDbContext db,
        CancellationToken ct)
    {
        var posts = await db.ClinicPosts
            .AsNoTracking()
            .Select(p => new PostDto(
                p.Id,
                p.Title,
                p.Content,
                p.Author,
                p.CreatedAt,
                p.UpdatedAt))
            .ToListAsync(ct);

        return new GetPostsResponse(posts);
    }
}
