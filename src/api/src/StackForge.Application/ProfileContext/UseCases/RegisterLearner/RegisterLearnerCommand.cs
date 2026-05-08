using StackForge.Application.Abstractions.Messaging;
using StackForge.Application.Shared.Results;

namespace StackForge.Application.ProfileContext.UseCases.RegisterLearner
{
    public sealed record RegisterLearnerCommand(Guid UserId, string FirstName, string LastName, DateOnly BirthDate)
        : ICommand<Result<RegisterLearnerResponse>>;

}
