using StackForge.Application.Abstractions.Messaging;
using StackForge.Application.Shared.Results;

namespace StackForge.Application.ProfileContext.UseCases.AddStackToMentor
{
    public sealed record AddStackToMentorCommand(Guid UserId, Guid StackId) 
        : ICommand<Result<AddStackToMentorResponse>>;
}
 