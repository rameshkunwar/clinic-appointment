namespace Clinic.API.Features.Posts.UpdatePost;

public record UpdatePostCommand(Guid Id, string Title, string Content, string Author);
