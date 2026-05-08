using FluentValidation;

namespace StackForge.Application.ProfileContext.UseCases.AddStackToMentor
{
    public sealed class AddStackToMentorCommandValidator : AbstractValidator<AddStackToMentorCommand>
    {
        public AddStackToMentorCommandValidator()
        {
            RuleFor(x => x.StackId).NotEmpty().WithMessage("Stack reference is required.");
        }
    }
}
