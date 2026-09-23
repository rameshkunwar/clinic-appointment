namespace Clinic.API.Features.Posts.CreatePost;

public record CreatePostCommand(string Title, string Content, string Author);
