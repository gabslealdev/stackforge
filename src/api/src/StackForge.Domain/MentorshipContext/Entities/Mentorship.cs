using StackForge.Domain.MentorshipContext.Enums;
using StackForge.Domain.MentorshipContext.Errors;
using StackForge.Domain.MentorshipContext.ValueObjects;
using StackForge.Domain.Shared.Entities;
using StackForge.Domain.Shared.Exceptions;

namespace StackForge.Domain.MentorshipContext.Entities;

public sealed class Mentorship : Entity
{
    public Guid MentorshipRequestId { get; private set; }
    public Guid MentorId { get; private set; }
    public Guid LearnerId { get; private set; }
    public Guid StackId { get; private set; }
    public Goal Goal { get; private set; } = null!;
    public MentorshipStatus Status { get; private set; }
    public DateTimeOffset StartedAt { get; private set; }
    public DateTimeOffset? CompletedAt { get; private set; }
    public DateTimeOffset? CancelledAt { get; private set; }

    private Mentorship(){}

    private Mentorship(
        Guid mentorshipRequestId,
        Guid mentorId,
        Guid learnerId,
        Guid stackId,
        Goal goal)
    {
        MentorshipRequestId = mentorshipRequestId;
        MentorId = mentorId;
        LearnerId = learnerId;
        StackId = stackId;
        Goal = goal;
        Status = MentorshipStatus.InProgress;
        StartedAt = DateTimeOffset.UtcNow;
    }

    private static void Validate(Guid mentorshipRequestId, Guid mentorId, Guid learnerId, Guid stackId, Goal goal)
    {
        DomainExceptionValidation.When(mentorshipRequestId == Guid.Empty, MentorshipErrors.RequestRequired);
        DomainExceptionValidation.When(mentorId == Guid.Empty, MentorshipErrors.MentorRequired);
        DomainExceptionValidation.When(learnerId == Guid.Empty, MentorshipErrors.LearnerRequired);
        DomainExceptionValidation.When(stackId == Guid.Empty, MentorshipErrors.StackRequired);
        DomainExceptionValidation.When(goal is null, GoalErrors.GoalRequired);
    }

    internal static Mentorship CreateFromAcceptedRequest(MentorshipRequest request)
    {
        if (request is null)
            throw new DomainExceptionValidation(MentorshipErrors.RequestRequired);

        Validate(request.Id, request.MentorId, request.LearnerId, request.StackId, request.Goal);

        return new Mentorship(
            request.Id,
            request.MentorId,
            request.LearnerId,
            request.StackId,
            request.Goal);
    }
}
