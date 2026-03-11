using StackForge.Domain.Identity.Entities;

namespace StackForge.Application.Identity.Interfaces.Repository
{
    public interface IUserRegistrationRepository
    {
        Task AddAsync(UserRegistration userRegistration);
        Task<UserRegistration?> GetByUserId(Guid userId);
        void UpdateAsync(UserRegistration userRegistration);
    }
}
 