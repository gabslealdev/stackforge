namespace StackForge.Api.Contracts.MentorshipContext.SearchMentorByStacks;

public record SearchMentorByStacksRequestDto(IReadOnlyList<Guid> StackIds);