namespace StackForge.Application.ProfileContext.UseCases.GetCurrentMentor
{
    public sealed record GetCurrentMentorResponse(
        Guid UserId, 
        string FullName, 
        string CourseName, 
        string Institution, 
        string? Bio, 
        string Availability, 
        IReadOnlyCollection<MentorStackResponse> Stacks
        );

    public sealed record MentorStackResponse(Guid Id, string Name, string Key);
}
