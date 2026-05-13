using StackForge.Application.Abstractions.Messaging;
using StackForge.Application.Shared.Results;

namespace StackForge.Application.MentorshipContext.UseCases.SearchStack;

public sealed record SearchStackQuery(string SearchTerm) : IQuery<Result<IReadOnlyList<SearchStackResponse>>>;

