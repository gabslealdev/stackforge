namespace StackForge.Application.MentorshipContext.UseCases.GetSentMentorshipRequest;

public sealed record GetSentMentorshipRequestResponse(
    Guid MentorshipRequestId,
    Guid MentorId,
    string MentorName,
    Guid StackId,
    string StackName,
    string Goal,
    string Status,
    DateTimeOffset CreatedAt,
    DateTimeOffset? DecidedAt);
