using StackForge.Domain.Shared.Errors;

namespace StackForge.Domain.Identity.Errors
{
    public static class UserError
    {
        public static readonly DomainError EmailRequired = new ("User.Email.Required", "Email is required.");

        public static readonly DomainError EmailInvalid = new ("User.Email.Invalid", "Email is invalid.");

        public static readonly DomainError PasswordRequired = new ("User.Password.Required", "Password is required.");

    }
}
