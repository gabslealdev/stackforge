using StackForge.Application.Abstractions.Messaging;
using StackForge.Application.MentorshipContext.Errors;
using StackForge.Application.MentorshipContext.Interfaces;
using StackForge.Application.ProfileContext.Interfaces;
using StackForge.Application.Shared.Results;

namespace StackForge.Application.MentorshipContext.UseCases.GetSentMentorshipRequest;

public sealed class GetSentMentorshipRequestHandler : IQueryHandler<GetSentMentorshipRequestQuery,
    Result<IReadOnlyList<GetSentMentorshipRequestResponse>>>
{
    private readonly ILearnerProfileRepository _learnerProfileRepository;
    private readonly IMentorshipRequestRepository _mentorshipRequestRepository;

    public GetSentMentorshipRequestHandler(
        ILearnerProfileRepository  learnerProfileRepository,
        IMentorshipRequestRepository mentorshipRequestRepository)
    {
        _learnerProfileRepository = learnerProfileRepository;
        _mentorshipRequestRepository = mentorshipRequestRepository;
    }
    public async Task<Result<IReadOnlyList<GetSentMentorshipRequestResponse>>> 
        HandleAsync(GetSentMentorshipRequestQuery query)
    {
        var learner = await _learnerProfileRepository.GetByUserIdAsync(query.UserId);

        if (learner is null)
            return Result<IReadOnlyList<
                GetSentMentorshipRequestResponse>>.Failure(SendMentorshipRequestErrors.LearnerNotFound);
        
        var requests = await _mentorshipRequestRepository.GetSentByMentorId(learner.Id);
        
        var response = requests.Select(request => new GetSentMentorshipRequestResponse(
            request.MentorRequestId,
            request.MentorId,
            request.MentorName,
            request.StackId,
            request.StackName,
            request.Goal,
            request.Status,
            request.CreatedAt,
            request.DecidedAt)).ToList();
        
        return Result<IReadOnlyList<GetSentMentorshipRequestResponse>>.Success(response);
    }
}
