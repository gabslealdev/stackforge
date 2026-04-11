namespace StackForge.Application.Profile.UseCases.UpdateMentorAvailability
{
    public sealed record UpdateMentorAvailabilityCommand(Guid UserId, bool IsAvailable);
}
