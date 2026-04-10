using Microsoft.EntityFrameworkCore;
using StackForge.Application.Profile.Interfaces;
using StackForge.Domain.Profile.Entities;
using StackForge.Infrastructure.Data.Context;

namespace StackForge.Infrastructure.Data.Repositories.Profile
{
    internal class MentorProfileRepository : IMentorProfileRepository
    {
        private readonly StackForgeDbContext _context;

        public MentorProfileRepository(StackForgeDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(MentorProfile mentorProfile)
        {
            await _context.Mentors.AddAsync(mentorProfile);
        }

        public async Task<bool> ExistsByUserIdAsync(Guid userId)
        {
            return await _context.Mentors.AnyAsync(x => x.UserId == userId);
        }

        public async Task<MentorProfile?> GetByUserIdAsync(Guid userId)
        {
            var mentorProfile = await _context.Mentors.FirstOrDefaultAsync(x => x.UserId == userId);

            return mentorProfile;
        }

        public void Update(MentorProfile mentorProfile)
        {
            _context.Mentors.Update(mentorProfile);
        }
    }
}
