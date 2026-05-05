namespace StackForge.Api.Contracts.ProfileContext.MentorProfile.RegisterMentor.Requests
{
    public sealed record RegisterMentorRequestDto(string UserId, string FirstName, string LastName, DateOnly BirthDate, 
            string CourseName, string Institution, int EducationStatus, DateOnly ConclusionDate, string? Bio);
}
