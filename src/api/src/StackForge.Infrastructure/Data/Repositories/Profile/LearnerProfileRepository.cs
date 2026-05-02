using Microsoft.EntityFrameworkCore;
using StackForge.Application.Profile.Interfaces;
using StackForge.Domain.Profile.Entities;
using StackForge.Infrastructure.Data.Context;

namespace StackForge.Infrastructure.Data.Repositories.Profile
{
    public class LearnerProfileRepository : ILearnerProfileRepository
    {
        private readonly StackForgeDbContext _context;
        public LearnerProfileRepository(StackForgeDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(LearnerProfile learnerProfile)
        {
            await _context.Learners.AddAsync(learnerProfile);
        }

        public async Task<bool> ExistsByUserIdAsync(Guid userId)
        {
            return await _context.Learners.AnyAsync(x => x.UserId == userId);
        }
    }
}
