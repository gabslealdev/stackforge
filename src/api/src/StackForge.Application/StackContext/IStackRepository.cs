using StackForge.Domain.StacksContext.Entities;

namespace StackForge.Application.StackContext
{
    public interface IStackRepository
    {
        Task<IReadOnlyList<Stack>> GetAllOrderedByNameAsync();

        Task<Stack?> GetByIdAsync(Guid stackId);
        
        Task<IReadOnlyList<Stack>> SearchByTermAsync(string term);
        
    }
}
