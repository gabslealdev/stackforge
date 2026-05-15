using StackForge.Application.Abstractions.Messaging;
using StackForge.Application.Shared.Results;
using StackForge.Application.StackContext;

namespace StackForge.Application.MentorshipContext.UseCases.SearchStack;

public sealed class SearchStackHandler : IQueryHandler<SearchStackQuery, Result<IReadOnlyList<SearchStackResponse>>>
{
    private readonly IStackRepository _stackRepository;

    public SearchStackHandler(IStackRepository stackRepository)
    {
        _stackRepository = stackRepository;
    }
    
    public async Task<Result<IReadOnlyList<SearchStackResponse>>> HandleAsync(SearchStackQuery query)
    {
        var stacks = await _stackRepository.SearchByTermAsync(query.SearchTerm);
        
        var response = stacks.Select(stack => new SearchStackResponse(stack.Id, stack.Name, stack.Key.Value))
            .ToList();

        return Result<IReadOnlyList<SearchStackResponse>>.Success(response);
    }
}