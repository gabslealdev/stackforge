namespace StackForge.Application.MentorshipContext.UseCases.GetReceivedMentorshipRequest;

public sealed record GetReceivedMentorshipRequestResponse(
    Guid MentorshipRequestId,
    Guid LearnerId,
    string LearnerName,
    Guid StackId,
    string StackName,
    string Goal,
    string Status,
    DateTimeOffset CreatedAt);