using FluentValidation;

namespace StackForge.Application.Profile.UseCases.AddStackToMentor
{
    public sealed class AddStackToMentorCommandValidator : AbstractValidator<AddStackToMentorCommand>
    {
        public AddStackToMentorCommandValidator()
        {
            RuleFor(x => x.StackId).NotEmpty().WithMessage("Stack reference is required.");
        }
    }
}
