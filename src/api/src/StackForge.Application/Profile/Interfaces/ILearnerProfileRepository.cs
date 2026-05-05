using StackForge.Domain.ProfileContext.Entities;

namespace StackForge.Application.Profile.Interfaces
{
    public interface ILearnerProfileRepository
    {
        Task AddAsync(LearnerProfile learnerProfile);
        Task<bool> ExistsByUserIdAsync(Guid userId);
    }
}
