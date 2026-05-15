
namespace StackForge.Application.MentorshipContext.UseCases.SearchMentorByStacks;

public sealed record SearchMentorByStacksResponse(
    Guid MentorId, 
    string FullName, 
    string CourseName, 
    string Institution, 
    IReadOnlyList<MentorStackResponse> Stacks
    );
    
public sealed record MentorStackResponse(Guid StackId, string Name, string Key);
