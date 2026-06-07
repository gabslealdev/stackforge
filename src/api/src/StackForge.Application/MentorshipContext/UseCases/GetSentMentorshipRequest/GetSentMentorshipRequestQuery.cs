using StackForge.Application.Abstractions.Messaging;
using StackForge.Application.Shared.Results;

namespace StackForge.Application.MentorshipContext.UseCases.GetSentMentorshipRequest;

public sealed record GetSentMentorshipRequestQuery(Guid UserId) 
    : IQuery<Result<IReadOnlyList<GetSentMentorshipRequestResponse>>>;