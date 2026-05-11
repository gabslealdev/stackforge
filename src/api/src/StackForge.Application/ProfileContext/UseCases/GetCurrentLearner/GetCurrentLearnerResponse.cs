namespace StackForge.Application.ProfileContext.UseCases.GetCurrentLearner;

public sealed record GetCurrentLearnerResponse(Guid UserId, string FullName);