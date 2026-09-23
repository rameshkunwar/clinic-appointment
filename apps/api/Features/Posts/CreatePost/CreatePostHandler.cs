using Clinic.API.Data;
using Clinic.API.Features.Posts;

namespace Clinic.API.Features.Posts.CreatePost;

public static class CreatePostHandler
{
    public static async Task<CreatePostResponse> Handle(
        CreatePostCommand command,
        AppDbContext db,
        CancellationToken ct)
    {
        var clinicPost = new ClinicPost
        {
            Id = Guid.NewGuid(),
            Title = command.Title,
            Content = command.Content,
            Author = command.Author,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        db.ClinicPosts.Add(clinicPost);
        await db.SaveChangesAsync(ct);

        return new CreatePostResponse(
            clinicPost.Id,
            clinicPost.Title,
            clinicPost.Content,
            clinicPost.Author,
            clinicPost.CreatedAt,
            clinicPost.UpdatedAt);
    }
}
