namespace Clinic.API.Features.Posts.UpdatePost;

public record UpdatePostResponse(Guid Id, string Title, string Content, string Author, DateTime CreatedAt, DateTime UpdatedAt);
