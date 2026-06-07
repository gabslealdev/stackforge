namespace StackForge.Api.Contracts.MentorshipContext.SearchMentorByStacks.Request;

public record SearchMentorByStacksRequestDto(IReadOnlyList<Guid> StackIds);