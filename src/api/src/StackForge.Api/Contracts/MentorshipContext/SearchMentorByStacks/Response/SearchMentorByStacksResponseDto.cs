namespace StackForge.Api.Contracts.MentorshipContext.SearchMentorByStacks.Response;

public sealed record SearchMentorByStacksResponseDto(
    Guid MentorId,
    string FullName,
    string CourseName,
    string Institution,
    IReadOnlyList<MentorStackResponseDto> Stacks);
    
public sealed record MentorStackResponseDto(
    Guid StackId,
    string Name,
    string Key
);