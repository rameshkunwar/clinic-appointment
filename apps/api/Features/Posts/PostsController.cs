using Wolverine;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Features.Posts
{
    using CreatePost;
    using GetPost;
    using GetPosts;
    using UpdatePost;
    using DeletePost;

    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IMessageBus _messageBus;

        public PostsController(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        [HttpPost]
        public async Task<ActionResult<CreatePostResponse>> CreatePost([FromBody] CreatePostCommand command)
        {
            var response = await _messageBus.InvokeAsync<CreatePostResponse>(command);
            return CreatedAtAction(nameof(GetPost), new { id = response.Id }, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetPostResponse>> GetPost(Guid id)
        {
            var response = await _messageBus.InvokeAsync<GetPostResponse>(new GetPostQuery(id));
            if (response is null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<GetPostsResponse>> GetPosts()
        {
            var response = await _messageBus.InvokeAsync<GetPostsResponse>(new GetPostsQuery());
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UpdatePostResponse>> UpdatePost(Guid id, [FromBody] UpdatePostCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            var response = await _messageBus.InvokeAsync<UpdatePostResponse>(command);
            if (response is null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DeletePostResponse>> DeletePost(Guid id)
        {
            var response = await _messageBus.InvokeAsync<DeletePostResponse>(new DeletePostCommand(id));
            if (!response.IsSuccess)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
