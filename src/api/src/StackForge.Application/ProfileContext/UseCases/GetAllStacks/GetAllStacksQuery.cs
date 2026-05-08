using StackForge.Application.Abstractions.Messaging;
using StackForge.Application.Shared.Results;

namespace StackForge.Application.ProfileContext.UseCases.GetAllStacks
{
    public sealed record GetAllStacksQuery: IQuery<Result<IReadOnlyList<GetAllStacksResponse>>>;
}
