using StackForge.Domain.Shared.Errors;

namespace StackForge.Domain.MentorshipContext.Errors;

public static class GoalErrors
{
    public static readonly DomainError GoalRequired = new DomainError("Goal.Required", "Goal is required.");
    public static readonly DomainError GoalTooShort = new DomainError("Goal.Minlength", "Goal must have at least 3 characters.");
    public static readonly DomainError GoalTooLong = new DomainError("Goal.Maxlength", "Goal must have at most 150 characters.");
}