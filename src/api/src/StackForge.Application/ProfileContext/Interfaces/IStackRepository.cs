using StackForge.Domain.StacksContext.Entities;

namespace StackForge.Application.ProfileContext.Interfaces
{
    public interface IStackRepository
    {
        Task<IReadOnlyList<Stack>> GetAllOrderedByNameAsync();

        Task<Stack?> GetByIdAsync(Guid stackId);
    }
}
