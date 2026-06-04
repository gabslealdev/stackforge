namespace StackForge.Application.MentorshipContext.UseCases.GetSentMentorshipRequest;

public sealed record SentMentorshipRequestReadModel(
    Guid MentorRequestId,
    Guid MentorId,
    string MentorName,
    Guid StackId,
    string StackName,
    string Goal,
    string Status,
    DateTimeOffset CreatedAt,
    DateTimeOffset? DecidedAt);