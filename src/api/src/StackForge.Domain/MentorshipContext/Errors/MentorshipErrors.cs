using StackForge.Domain.Shared.Errors;

namespace StackForge.Domain.MentorshipContext.Errors;

public static class MentorshipErrors
{
    public static readonly DomainError LearnerRequired = 
        new DomainError("Learner.Reference.Required", "Learner reference is required.");
    
    public static readonly DomainError MentorRequired = 
        new DomainError("Mentor.Required", "Mentor reference is required.");
    
    public static readonly DomainError StackRequired = 
        new DomainError("Stack.Required", "Stack reference is required.");
    
    public static readonly DomainError RequestRequired =
        new DomainError("Request.Required", "Request reference is required.");

    public static readonly DomainError RequestMustBePending =
        new DomainError("MentorshipRequest.Status.MustBePending", "Mentorship request must be pending.");
}
