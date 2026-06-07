using FluentValidation;

namespace StackForge.Application.MentorshipContext.UseCases.SendMentorshipRequest;

public sealed class SendMentorshipRequestValidator : AbstractValidator<SendMentorshipRequestCommand>
{
    public SendMentorshipRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User  reference is required.");
        
        RuleFor(x => x.MentorId)
            .NotEmpty().WithMessage("Mentor reference is required.");
        
        RuleFor(x => x.StackId)
            .NotEmpty().WithMessage("Stack reference is required.");

        RuleFor(x => x.Goal)
            .NotEmpty().WithMessage("Goal is required.")
            .MinimumLength(3).WithMessage("Goal must be at least 3 characters long.")
            .MaximumLength(150).WithMessage("Goal must be no longer than 150 characters long.");

    }
}