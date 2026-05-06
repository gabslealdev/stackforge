using StackForge.Application.Abstractions.Messaging;
using StackForge.Application.Shared.Results;

namespace StackForge.Application.Profile.UseCases.RegisterMentor
{
    public sealed record RegisterMentorCommand(
        Guid UserId,
        string FirstName,
        string LastName,
        DateOnly BirthDate,
        string CourseName,
        string Institution,
        int EducationStatus,
        DateOnly ConclusionDate,
        string? Bio) : ICommand<Result<RegisterMentorResponse>>;
}
