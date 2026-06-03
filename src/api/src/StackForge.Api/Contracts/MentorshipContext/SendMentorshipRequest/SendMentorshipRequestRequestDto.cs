namespace StackForge.Api.Contracts.MentorshipContext.SendMentorshipRequest;

public sealed record SendMentorshipRequestRequestDto(
    Guid MentorId,
    Guid StackId,
    string Goal);