using FluentValidation;

namespace Clinic.API.Features.Posts.CreatePost;

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Content).NotEmpty();
        RuleFor(x => x.Author).NotEmpty().MaximumLength(50);
    }
}
