using StackForge.Application.Abstractions.Messaging;
using StackForge.Application.Identity.UseCases.LoginUser;
using StackForge.Application.Profile.UseCases.GetAllStacks;
using StackForge.Application.Shared.Results;

namespace StackForge.Application.Profile.UseCases.GetCurrentMentor
{
    public sealed record GetCurrentMentorQuery(Guid UserId) : IQuery<Result<GetCurrentMentorResponse>>;
}
