using StackForge.Domain.MentorshipContext.Enums;
using StackForge.Domain.MentorshipContext.Errors;
using StackForge.Domain.MentorshipContext.ValueObjects;
using StackForge.Domain.Shared.Entities;
using StackForge.Domain.Shared.Exceptions;

namespace StackForge.Domain.MentorshipContext.Entities;

public sealed class MentorshipRequest : Entity
{
    public Guid LearnerId { get; private set; }
    public Guid MentorId { get; private set; }
    public Guid StackId { get; private set; }
    public Goal Goal { get; private set; } = null!;
    public DateTimeOffset CreatedAt { get; private set; }
    public MentorshipRequestStatus Status { get; private set; }
    public DateTimeOffset? DecidedAt { get; private set; }

    private MentorshipRequest(){}

    private MentorshipRequest(Guid learnerId, Guid mentorId, Guid stackId, Goal goal)
    {
        LearnerId = learnerId;
        MentorId = mentorId;
        StackId = stackId;
        Goal = goal;
        CreatedAt = DateTimeOffset.UtcNow;
        Status = MentorshipRequestStatus.Pending;
    }

    private static void Validate(Guid learnerId, Guid mentorId, Guid stackId, Goal goal)
    {
        DomainExceptionValidation.When(learnerId == Guid.Empty, MentorshipErrors.LearnerRequired);
        DomainExceptionValidation.When(mentorId == Guid.Empty, MentorshipErrors.MentorRequired);
        DomainExceptionValidation.When(stackId == Guid.Empty, MentorshipErrors.StackRequired);
        DomainExceptionValidation.When(goal is null, GoalErrors.GoalRequired);
    }

    public static MentorshipRequest Create(Guid learnerId, Guid mentorId, Guid stackId, Goal goal)
    {
        Validate(learnerId, mentorId, stackId, goal);
        return new MentorshipRequest(learnerId, mentorId, stackId, goal);
    }
    
    public void Reject()
    {
        EnsurePending();

        Status = MentorshipRequestStatus.Rejected;
        DecidedAt = DateTimeOffset.UtcNow;
    }

    public Mentorship Accept()
    {
        EnsurePending();

        Status = MentorshipRequestStatus.Accepted;
        DecidedAt = DateTimeOffset.UtcNow;
        
        return Mentorship.CreateFromAcceptedRequest(this);
    }

    public void Cancel()
    {
        EnsurePending();

        Status = MentorshipRequestStatus.Cancelled;
        DecidedAt = DateTimeOffset.UtcNow;
    }

    private void EnsurePending()
    {
        DomainExceptionValidation.When(Status != MentorshipRequestStatus.Pending, MentorshipErrors.RequestMustBePending);
    }
    
}
