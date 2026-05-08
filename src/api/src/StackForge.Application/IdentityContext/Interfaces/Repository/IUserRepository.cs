using StackForge.Domain.IdentityContext.Entities;
using StackForge.Domain.IdentityContext.ValueObjects;

namespace StackForge.Application.IdentityContext.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<bool> ExistsByEmailAsync(Email email);
        Task AddAsync(User user);
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByEmailAsync(Email email);
    }
}
