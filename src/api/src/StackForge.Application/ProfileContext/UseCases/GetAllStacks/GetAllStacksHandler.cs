using StackForge.Application.Abstractions.Messaging;
using StackForge.Application.ProfileContext.Interfaces;
using StackForge.Application.Shared.Results;

namespace StackForge.Application.ProfileContext.UseCases.GetAllStacks
{
    public sealed class GetAllStacksHandler 
        : IQueryHandler<GetAllStacksQuery, Result<IReadOnlyList<GetAllStacksResponse>>>
    {
        private readonly IStackRepository _stackRepository;

        public GetAllStacksHandler(IStackRepository stackRepository)
        {
            _stackRepository = stackRepository;
        }

        public async Task<Result<IReadOnlyList<GetAllStacksResponse>>> HandleAsync(GetAllStacksQuery query)
        {
            var stacks = await _stackRepository.GetAllOrderedByNameAsync();

            IReadOnlyList<GetAllStacksResponse> response = stacks.Select(stack => new
                GetAllStacksResponse(
                    stack.Id, 
                    stack.Name, 
                    stack.Key.Value))
                .ToList();

            return Result<IReadOnlyList<GetAllStacksResponse>>.Success(response);
        }
    }
}
