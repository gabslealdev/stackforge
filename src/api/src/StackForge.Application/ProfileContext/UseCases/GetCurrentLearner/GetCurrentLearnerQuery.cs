using StackForge.Application.Abstractions.Messaging;
using StackForge.Application.Shared.Results;

namespace StackForge.Application.ProfileContext.UseCases.GetCurrentLearner;

public sealed record GetCurrentLearnerQuery(Guid UserId) : IQuery<Result<GetCurrentLearnerResponse>>;