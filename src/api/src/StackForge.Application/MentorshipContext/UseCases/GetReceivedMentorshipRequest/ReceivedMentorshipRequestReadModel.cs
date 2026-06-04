namespace StackForge.Application.MentorshipContext.UseCases;

public sealed record ReceivedMentorshipRequestReadModel(
    Guid MentorshipRequestId,
    Guid LearnerId,
    string LearnerName,
    Guid StackId,
    string StackName,
    string Goal,
    string Status,
    DateTimeOffset CreatedAt
    );