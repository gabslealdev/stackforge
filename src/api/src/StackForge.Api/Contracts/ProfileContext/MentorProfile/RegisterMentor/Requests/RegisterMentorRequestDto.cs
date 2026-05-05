namespace StackForge.Api.Contracts.ProfileContext.MentorProfile.Requests
{
    public sealed record RegisterMentorRequestDto(string FirstName, string LastName, DateOnly BirthDate, 
            string CourseName, string Institution, string EducationStatus, DateOnly ConclusionDate, string? Bio);
}
