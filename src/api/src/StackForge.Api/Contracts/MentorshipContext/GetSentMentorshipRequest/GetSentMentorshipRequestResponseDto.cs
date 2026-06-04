namespace StackForge.Api.Contracts.MentorshipContext.GetSentMentorshipRequest;

public record GetSentMentorshipRequestResponseDto(
    Guid MentorshipRequestId,
    Guid MentorId,
    string MentorName,
    Guid StackId,
    string StackName,
    string Goal,
    string Status,
    DateTimeOffset CreatedAt,
    DateTimeOffset? DecidedAt);