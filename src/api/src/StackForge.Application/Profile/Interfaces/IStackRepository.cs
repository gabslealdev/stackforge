using StackForge.Domain.Stacks.Entities;

namespace StackForge.Application.Profile.Interfaces
{
    public interface IStackRepository
    {
        Task<IReadOnlyList<Stack>> GetAllOrderedByNameAsync();

        Task<Stack?> GetByIdAsync(Guid stackId);
    }
}
