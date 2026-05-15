namespace StackForge.Api.Contracts.MentorshipContext.SearchStack.Response;

public record SearchStackResponseDto(
    Guid StackId,
    string Name,
    string Key
    );