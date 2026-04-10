namespace StackForge.Api.Contracts.Profile.MentorProfile.Requests
{
    public sealed record RegisterMentorRequest
    {
        public Guid UserId { get; init; }
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public DateOnly BirthDate { get; init; }
        public string CourseName { get; init; } = string.Empty;
        public string Institution { get; init; } = string.Empty;
        public int EducationStatus { get; init; }
        public DateOnly ConclusionDate { get; init; }
        public string? Bio { get; init; }

    }
}
