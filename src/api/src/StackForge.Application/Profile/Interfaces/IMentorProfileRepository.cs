using StackForge.Domain.Profile.Entities;

namespace StackForge.Application.Profile.Interfaces
{
    public interface IMentorProfileRepository
    {
        Task AddAsync(MentorProfile mentorProfile);

        Task<bool> ExistsByUserIdAsync(Guid userId);

        Task<MentorProfile?> GetByUserIdAsync(Guid userId);

        Task<MentorProfile?> GetWithStacksByUserIdAsync(Guid userId);

        void Update(MentorProfile mentorProfile);

        
    }
}
