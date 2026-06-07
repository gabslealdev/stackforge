using StackForge.Application.Abstractions.Messaging;
using StackForge.Application.Abstractions.Persistance;
using StackForge.Application.MentorshipContext.Errors;
using StackForge.Application.MentorshipContext.Interfaces;
using StackForge.Application.ProfileContext.Interfaces;
using StackForge.Application.Shared.Results;
using StackForge.Application.StackContext;
using StackForge.Domain.MentorshipContext.Entities;
using StackForge.Domain.MentorshipContext.ValueObjects;
using StackForge.Domain.ProfileContext.Enums;

namespace StackForge.Application.MentorshipContext.UseCases.SendMentorshipRequest;

public sealed class SendMentorshipRequestHandler : ICommandHandler<SendMentorshipRequestCommand,
    Result<SendMentorshipRequestResponse>>
{
    private readonly ILearnerProfileRepository _learnerProfileRepository;
    private readonly IMentorProfileRepository _mentorProfileRepository;
    private readonly IStackRepository _stackRepository;
    private readonly IMentorshipRequestRepository _mentorshipRequestRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SendMentorshipRequestHandler(
        ILearnerProfileRepository learnerProfileRepository,
        IMentorProfileRepository mentorProfileRepository,
        IStackRepository stackRepository,
        IMentorshipRequestRepository mentorshipRequestRepository,
        IUnitOfWork unitOfWork)
    {
        _learnerProfileRepository = learnerProfileRepository;
        _mentorProfileRepository = mentorProfileRepository;
        _stackRepository = stackRepository;
        _mentorshipRequestRepository = mentorshipRequestRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<SendMentorshipRequestResponse>> HandleAsync(SendMentorshipRequestCommand command)
    {
        var learner = await _learnerProfileRepository.GetByUserIdAsync(command.UserId);

        if (learner is null)
            return Result<SendMentorshipRequestResponse>.Failure(SendMentorshipRequestErrors.LearnerNotFound);

        var mentor = await _mentorProfileRepository.GetByMentorIdAsync(command.MentorId);

        if (mentor is null)
            return Result<SendMentorshipRequestResponse>.Failure(SendMentorshipRequestErrors.MentorNotFound);

        if (mentor.Availability != AvailabilityStatus.Available)
            return Result<SendMentorshipRequestResponse>.Failure(SendMentorshipRequestErrors.MentorUnavailable);

        var stack = await _stackRepository.GetByIdAsync(command.StackId);

        if (stack is null)
            return Result<SendMentorshipRequestResponse>.Failure(SendMentorshipRequestErrors.StackNotFound);

        var goal = Goal.Create(command.Goal);

        var mentorshipRequest = MentorshipRequest.Create(learner.Id, mentor.Id, stack.Id, goal);

        await _mentorshipRequestRepository.AddAsync(mentorshipRequest);
        await _unitOfWork.SaveChangesAsync();

        var response = new SendMentorshipRequestResponse(mentorshipRequest.Id);

        return Result<SendMentorshipRequestResponse>.Success(response);
    }
}