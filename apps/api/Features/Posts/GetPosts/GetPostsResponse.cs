using Clinic.API.Features.Posts;

namespace Clinic.API.Features.Posts.GetPosts;

public record GetPostsResponse(List<PostDto> Posts);

public record PostDto(Guid Id, string Title, string Content, string Author, DateTime CreatedAt, DateTime UpdatedAt);
