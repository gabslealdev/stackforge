using StackForge.Application.Abstractions.Messaging;
using StackForge.Application.Shared.Results;

namespace StackForge.Application.ProfileContext.UseCases.GetCurrentMentor
{
    public sealed record GetCurrentMentorQuery(Guid UserId) : IQuery<Result<GetCurrentMentorResponse>>;
}
