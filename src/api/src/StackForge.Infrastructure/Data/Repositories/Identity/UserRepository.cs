using Microsoft.EntityFrameworkCore;
using StackForge.Application.Identity.Interfaces.Repository;
using StackForge.Domain.Identity.Entities;
using StackForge.Domain.Identity.ValueObjects;
using StackForge.Infrastructure.Data.Context;

namespace StackForge.Infrastructure.Data.Repositories.Identity
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly StackForgeDbContext _context;

        public UserRepository(StackForgeDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<bool> ExistsByEmailAsync(Email email)
        {
            return await _context.Users.AnyAsync(user => user.Email == email);
        }

        public async Task<User?> GetByEmailAsync(Email email)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
        }
    }
}
