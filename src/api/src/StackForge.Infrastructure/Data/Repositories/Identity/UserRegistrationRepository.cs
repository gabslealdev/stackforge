using Microsoft.EntityFrameworkCore;
using StackForge.Application.Identity.Interfaces.Repository;
using StackForge.Domain.IdentityContext.Entities;
using StackForge.Infrastructure.Data.Context;

namespace StackForge.Infrastructure.Data.Repositories.Identity
{
    public sealed class UserRegistrationRepository : IUserRegistrationRepository
    {
        private readonly StackForgeDbContext _context;

        public UserRegistrationRepository(StackForgeDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(UserRegistration userRegistration)
        {
            await _context.UserRegistrations.AddAsync(userRegistration);
        }

        public async Task<UserRegistration?> GetByUserIdAsync(Guid userId)
        {
            return await _context.UserRegistrations.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public void Update(UserRegistration userRegistration)
        {
            _context.UserRegistrations.Update(userRegistration);
        }
    }
}
