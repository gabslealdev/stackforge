using StackForge.Domain.Identity.Entities;

namespace StackForge.Application.Identity.Interfaces.Repository
{
    public interface IUserRegistrationRepository
    {
        Task AddAsync(UserRegistration userRegistration);
        Task<UserRegistration?> GetByUserIdAsync(Guid userId);
        void Update(UserRegistration userRegistration);
    }
}
 