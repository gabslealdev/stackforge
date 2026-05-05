using StackForge.Domain.Shared.Errors;

namespace StackForge.Domain.StacksContext.Errors
{
    public static class StackErrors
    {
            public readonly static DomainError StackNameRequired = new(Code: "Stack.Name.Required", Message: "Stack name is required.");
            public readonly static DomainError StackKeyRequired = new(Code: "Stack.Key.Required", Message: "Stack key is required.");
            public readonly static DomainError StackNameTooLong = new(Code: "Stack.Name.IsTooLong", Message: "Stack name must be at most 20 characters long.");
            public readonly static DomainError StackKeyTooLong = new(Code: "Stack.Key.IsTooLong", Message: "Stack key must be at most 20 characters long.");
    }
}
