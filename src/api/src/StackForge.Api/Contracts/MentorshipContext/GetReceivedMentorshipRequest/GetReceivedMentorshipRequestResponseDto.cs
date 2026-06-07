namespace StackForge.Api.Contracts.MentorshipContext.GetReceivedMentorshipRequest;

public sealed record GetReceivedMentorshipRequestResponseDto(
    Guid MentorshipRequestId,
    Guid LearnerId,
    string LearnerName,
    Guid StackId,
    string StackName,
    string Goal,
    string Status,
    DateTimeOffset CreatedAt);