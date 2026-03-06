using StackForge.Domain.Shared.Errors;

namespace StackForge.Domain.Identity.Errors
{
    public static class UserError
    {
        public static readonly DomainError EmailRequired = new (Code: "User.Email.Required", Message: "Email is required.");

        public static readonly DomainError EmailInvalid = new (Code: "User.Email.Invalid", Message: "Email is invalid.");

        public static readonly DomainError PasswordRequired = new (Code: "User.Password.Required", Message: "Password is required.");

    }
}
