using StackForge.Domain.MentorshipContext.Errors;
using StackForge.Domain.Shared.Exceptions;
using StackForge.Domain.Shared.ValueObjects;

namespace StackForge.Domain.MentorshipContext.ValueObjects;

public sealed record Goal : ValueObject
{
    private const int MinLength = 3;
    private const int MaxLength = 150;

    public string Value { get; private set; } = string.Empty;
    
    private Goal(){}

    private Goal(string value)
    {
        Value = value;
    }

    private static void Validate(string value)
    {
        DomainExceptionValidation.When(string.IsNullOrWhiteSpace(value), GoalErrors.GoalRequired);
        DomainExceptionValidation.When(value.Length < MinLength, GoalErrors.GoalTooShort);
        DomainExceptionValidation.When(value.Length > MaxLength, GoalErrors.GoalTooLong);
    }

    public static Goal Create(string value)
    {
        var normalizedValue = Normalize(value);
        Validate(normalizedValue);

        return new Goal(normalizedValue);
    }

    public override string ToString() 
        => Value;

    private static string Normalize(string value)
        => value?.Trim() ?? string.Empty;
}
