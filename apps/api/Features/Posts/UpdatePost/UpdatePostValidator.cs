using FluentValidation;

namespace Clinic.API.Features.Posts.UpdatePost;

public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
{
    public UpdatePostCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Content).NotEmpty();
        RuleFor(x => x.Author).NotEmpty().MaximumLength(50);
    }
}
