using StackForge.Domain.ProfileContext.Entities;

namespace StackForge.Application.ProfileContext.Interfaces
{
    public interface ILearnerProfileRepository
    {
        Task AddAsync(LearnerProfile learnerProfile);
        Task<bool> ExistsByUserIdAsync(Guid userId);
    }
}
