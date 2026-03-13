using FluentValidation;

namespace StackForge.Application.Profile.UseCases.RegisterMentor
{
    public sealed class RegisterMentorCommandValidator : AbstractValidator<RegisterMentorCommand>
    {
        public RegisterMentorCommandValidator()
        {
            RuleFor(m => m.UserId)
                .NotEmpty().WithMessage("User reference is required.");

            RuleFor(m => m.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MinimumLength(3).WithMessage("First name must be at least 3 characters long.")
                .MaximumLength(80).WithMessage("First name must be at most 80 characters long.");

            RuleFor(m => m.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MinimumLength(3).WithMessage("Last name must be at least 3 characters long.")
                .MaximumLength(80).WithMessage("Last name must be at most 80 characters long.");

            RuleFor(m => m.BirthDate)
                .NotEmpty().WithMessage("Birh Date is required.")
                .LessThan(DateOnly.FromDateTime(DateTime.UtcNow)).WithMessage("Birth date cannot be in the future.");

            RuleFor(m => m.CourseName)
                 .NotEmpty().WithMessage("Course name is required.")
                .MinimumLength(2).WithMessage("Course name must be at least 2 characters long.")
                .MaximumLength(100).WithMessage("Last name must be at most 100 characters long.");

            RuleFor(m => m.Institution)
                .NotEmpty().WithMessage("Institution name is required.")
                .MinimumLength(2).WithMessage("Institution name must be at least 2 characters long.")
                .MaximumLength(100).WithMessage("Institution name must be at most 100 characters long.");

            RuleFor(m => m.ConclusionDate)
                .NotEmpty().WithMessage("Conclusion date is required.");

            RuleFor(m => m.Bio)
                .MinimumLength(10).WithMessage("Bio must be at least 10 characters long.")
                .MaximumLength(100).WithMessage("Bio must be at most 100 characters long.")
                .When(x => !string.IsNullOrWhiteSpace(x.Bio));
        }
    }
}
