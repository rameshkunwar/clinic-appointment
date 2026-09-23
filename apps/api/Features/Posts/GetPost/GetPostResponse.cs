namespace Clinic.API.Features.Posts.GetPost;

public record GetPostResponse(Guid Id, string Title, string Content, string Author, DateTime CreatedAt, DateTime UpdatedAt);
