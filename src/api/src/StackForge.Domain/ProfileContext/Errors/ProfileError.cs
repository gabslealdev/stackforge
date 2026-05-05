using StackForge.Domain.Shared.Errors;

namespace StackForge.Domain.ProfileContext.Errors
{
    public static class ProfileError
    {
        public static readonly DomainError FirstNameRequired = new (Code: "Profile.FirstName.Required", Message: "First name is required.");
        public static readonly DomainError LastNameRequired = new (Code: "Profile.LastName.Required", Message: "Last name is required.");
        public static readonly DomainError UserIdRequired = new (Code: "Profile.UserId.Required", Message: "User is required.");
        public static readonly DomainError FirstNameTooShort = new (Code: "Profile.FirstName.TooShort", Message: "First name must be at least 3 characters long.");
        public static readonly DomainError FirstNameTooLong = new (Code: "Profile.FirstName.TooLong", Message: "First name must be at most 80 characters long.");
        public static readonly DomainError LastNameTooShort = new (Code: "Profile.LastName.TooShort", Message: "Last name must be at least 3 characters long.");
        public static readonly DomainError LastNameTooLong = new (Code: "Profile.LastName.TooLong", Message: "Last name must be at most 80 characters long.");
        public static readonly DomainError BirthDateInFuture = new (Code: "Profile.BirthDate.InFuture", Message: "Birth date cannot be in the future.");

    }
}
