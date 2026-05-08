using FluentValidation;

namespace StackForge.Application.IdentityContext.UseCases.LoginUser
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}
