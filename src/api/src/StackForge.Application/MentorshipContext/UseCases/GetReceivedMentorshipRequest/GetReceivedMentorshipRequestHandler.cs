using StackForge.Application.Abstractions.Messaging;
using StackForge.Application.MentorshipContext.Errors;
using StackForge.Application.MentorshipContext.Interfaces;
using StackForge.Application.ProfileContext.Interfaces;
using StackForge.Application.Shared.Results;

namespace StackForge.Application.MentorshipContext.UseCases.GetReceivedMentorshipRequest;

public class GetReceivedMentorshipRequestHandler : IQueryHandler<GetReceivedMentorshipRequestQuery, 
    Result<IReadOnlyList<GetReceivedMentorshipRequestResponse>>>
{
    private readonly IMentorProfileRepository _mentorProfileRepository;
    private readonly IMentorshipRequestRepository _mentorshipRequestRepository;

    public GetReceivedMentorshipRequestHandler(IMentorProfileRepository mentorProfileRepository, IMentorshipRequestRepository  mentorshipRequestRepository)
    {
        _mentorshipRequestRepository = mentorshipRequestRepository;
        _mentorProfileRepository = mentorProfileRepository;
    }
    public async Task<Result<IReadOnlyList<GetReceivedMentorshipRequestResponse>>> HandleAsync(GetReceivedMentorshipRequestQuery query)
    {
        var mentor = await _mentorProfileRepository.GetByUserIdAsync(query.UserId);
        
        if (mentor is null)
            return Result<IReadOnlyList<GetReceivedMentorshipRequestResponse>>.Failure(SendMentorshipRequestErrors
                .MentorNotFound);

        var requests = await _mentorshipRequestRepository.GetReceivedByMentorId(mentor.Id);
        
        var response = requests.Select(request => new GetReceivedMentorshipRequestResponse(
            request.MentorshipRequestId,
            request.LearnerId,
            request.LearnerName,
            request.StackId,
            request.StackName,
            request.Goal,
            request.Status,
            request.CreatedAt)).ToList();
        
        return Result<IReadOnlyList<GetReceivedMentorshipRequestResponse>>.Success(response);
        
    }
}