using StackForge.Domain.Shared.Errors;

namespace StackForge.Domain.Profile.Errors
{
    public static class MentorError
    {
        public readonly static DomainError StackRequired = new (Code: "Mentor.Stack.Required", Message: "At least one stack is required.");
        public readonly static DomainError StackAlreadyAdded = new (Code: "Mentor.Stack.AlreadyAdded", Message: "This stack has already been added to the mentor profile.");
    }
}
