using StackForge.Application.Abstractions.Messaging;
using StackForge.Application.ProfileContext.Errors;
using StackForge.Application.ProfileContext.Interfaces;
using StackForge.Application.Shared.Results;

namespace StackForge.Application.ProfileContext.UseCases.GetCurrentLearner;

public sealed class GetCurrentLearnerHandler : IQueryHandler<GetCurrentLearnerQuery, Result<GetCurrentLearnerResponse>>
{
    private readonly ILearnerProfileRepository _learnerProfileRepository;

    public GetCurrentLearnerHandler(ILearnerProfileRepository  learnerProfileRepository)
    {
        _learnerProfileRepository = learnerProfileRepository;
    }
    
    
    public async Task<Result<GetCurrentLearnerResponse>> HandleAsync(GetCurrentLearnerQuery query)
    {
        var learner = await _learnerProfileRepository.GetByUserIdAsync(query.UserId);

        if (learner is null)
            return Result<GetCurrentLearnerResponse>.Failure(ProfileApplicationErrors.LearnerNotFound);
       
        var response = new GetCurrentLearnerResponse(learner.UserId, learner.Name.ToString());
        
        return Result<GetCurrentLearnerResponse>.Success(response);
    }
}