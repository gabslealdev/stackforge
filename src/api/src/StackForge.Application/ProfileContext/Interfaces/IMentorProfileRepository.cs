using StackForge.Application.MentorshipContext.UseCases.SearchMentorByStacks;
using StackForge.Domain.ProfileContext.Entities;

namespace StackForge.Application.ProfileContext.Interfaces
{
    public interface IMentorProfileRepository
    {
        Task AddAsync(MentorProfile mentorProfile);

        Task<bool> ExistsByUserIdAsync(Guid userId);

        Task<MentorProfile?> GetByUserIdAsync(Guid userId);
        
        Task<MentorProfile?> GetByMentorIdAsync(Guid mentorId);

        Task<MentorProfile?> GetWithStacksByUserIdAsync(Guid userId);

        void Update(MentorProfile mentorProfile);
        
        Task<IReadOnlyList<MentorProfile>> SearchMentorByStacksAsync(IReadOnlyList<Guid> stackIds);

        
    }
}
