using StackForge.Domain.Profile.Entities;

namespace StackForge.Application.Profile.Interfaces
{
    public interface IMentorProfileRepository
    {
        Task AddAsync(MentorProfile mentorProfile);

        Task<bool> ExistsByUserIdAsync(Guid userId);
    }
}
