using StackForge.Domain.Shared.Errors;

namespace StackForge.Domain.Profile.Errors
{
    public static class MentorError
    {
        public readonly static DomainError StackRequired = new (Code: "Mentor.Stack.Required", Message: "At least one stack is required.");
        public readonly static DomainError StackNotFound = new (Code: "Mentor.Stack.NotFound", Message: "This stack cannot be found.");
        public readonly static DomainError StackOnlyOne = new (Code: "Mentor.Stack.OnlyOne", Message: "Your profile must include at least one stack.");
        public readonly static DomainError StackAlreadyAdded = new (Code: "Mentor.Stack.AlreadyAdded", Message: "This stack has already been added to the mentor profile.");
        public readonly static DomainError EducationRequired = new (Code: "Mentor.Education.Required", Message: "Education information is required for a mentor profile.");
        public readonly static DomainError BioTooShort = new (Code: "Mentor.Bio.TooShort", Message: "The bio must be at least 10 characters long.");
        public readonly static DomainError BioTooLong = new (Code: "Mentor.Bio.TooLong", Message: "The bio must be no more than 500 characters long.");
    }
}
