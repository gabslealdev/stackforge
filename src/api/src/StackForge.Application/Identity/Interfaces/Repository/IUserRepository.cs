using StackForge.Domain.Identity.Entities;
using StackForge.Domain.Identity.ValueObjects;

namespace StackForge.Application.Identity.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<bool> ExistsByEmailAsync(Email email);
        Task AddAsync(User user);
        Task<User?> GetByIdAsync(Guid id);
    }
}
