namespace Clinic.API.Features.Posts.CreatePost;

public record CreatePostResponse(Guid Id, string Title, string Content, string Author, DateTime CreatedAt, DateTime UpdatedAt);
