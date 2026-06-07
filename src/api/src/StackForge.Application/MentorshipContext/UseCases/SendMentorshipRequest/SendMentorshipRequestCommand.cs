using StackForge.Application.Abstractions.Messaging;
using StackForge.Application.Shared.Results;

namespace StackForge.Application.MentorshipContext.UseCases.SendMentorshipRequest;

public sealed record SendMentorshipRequestCommand(
    Guid UserId, 
    Guid MentorId, 
    Guid StackId, 
    string Goal) : ICommand<Result<SendMentorshipRequestResponse>>;