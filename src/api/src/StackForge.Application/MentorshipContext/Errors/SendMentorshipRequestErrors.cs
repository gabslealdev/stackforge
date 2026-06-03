using StackForge.Application.Shared.Results;

namespace StackForge.Application.MentorshipContext.Errors;

public class SendMentorshipRequestErrors
{
    public static readonly Error LearnerNotFound = new Error("Learner.NotFound", "The learner was not found.");
    public static readonly Error MentorNotFound = new Error("Mentor.NotFound", "The mentor was not found.");
    public static readonly Error StackNotFound = new Error("Stack.NotFound", "The stack was not found.");
    public static readonly Error MentorUnavailable = new Error("Mentor.Unavailable", "The mentor was not available.");
}