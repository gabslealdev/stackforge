using StackForge.Application.Abstractions.Messaging;
using StackForge.Application.MentorshipContext.UseCases.SearchStack;
using StackForge.Application.Shared.Results;
using StackForge.Domain.StacksContext.Entities;

namespace StackForge.Application.MentorshipContext.UseCases.SearchMentorByStacks;

public sealed record SearchMentorByStacksQuery(IReadOnlyList<Guid> StackIds) 
    : IQuery<Result<IReadOnlyList<SearchMentorByStacksResponse>>>;