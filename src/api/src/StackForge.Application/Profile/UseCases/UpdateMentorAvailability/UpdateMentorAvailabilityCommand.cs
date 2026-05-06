using StackForge.Application.Abstractions.Messaging;
using StackForge.Application.Shared.Results;

namespace StackForge.Application.Profile.UseCases.UpdateMentorAvailability
{
    public sealed record UpdateMentorAvailabilityCommand(Guid UserId, bool IsAvailable) 
        : ICommand<Result>;
}
