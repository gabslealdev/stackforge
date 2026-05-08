using FluentValidation;

namespace StackForge.Application.ProfileContext.UseCases.RegisterLearner
{
    public sealed class RegisterLearnerCommandValidator : AbstractValidator<RegisterLearnerCommand>
    {
        public RegisterLearnerCommandValidator()
        {
            RuleFor(l => l.UserId)
                .NotEmpty().WithMessage("User reference is required.");

            RuleFor(l => l.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MinimumLength(3).WithMessage("First name must be at least 3 characters long.")
                .MaximumLength(80).WithMessage("First name must be at most 80 characters long.");

            RuleFor(l => l.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MinimumLength(3).WithMessage("Last name must be at least 3 characters long.")
                .MaximumLength(80).WithMessage("Last name must be at most 80 characters long.");

            RuleFor(l => l.BirthDate)
                .NotEmpty().WithMessage("Birh Date is required.")
                .LessThan(DateOnly.FromDateTime(DateTime.UtcNow)).WithMessage("Birth date cannot be in the future.");
        }
    }
}
