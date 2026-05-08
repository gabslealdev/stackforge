using StackForge.Domain.IdentityContext.Entities;

namespace StackForge.Application.IdentityContext.Interfaces.Repository
{
    public interface IUserRegistrationRepository
    {
        Task AddAsync(UserRegistration userRegistration);
        Task<UserRegistration?> GetByUserIdAsync(Guid userId);
        void Update(UserRegistration userRegistration);
    }
}
 