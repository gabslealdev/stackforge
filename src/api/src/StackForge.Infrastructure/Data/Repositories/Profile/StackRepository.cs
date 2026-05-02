using Microsoft.EntityFrameworkCore;
using StackForge.Application.Profile.Interfaces;
using StackForge.Domain.Stacks.Entities;
using StackForge.Infrastructure.Data.Context;

namespace StackForge.Infrastructure.Data.Repositories.Profile
{
    public sealed class StackRepository : IStackRepository
    {
        private readonly StackForgeDbContext _context;

        public StackRepository(StackForgeDbContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<Stack>> GetAllOrderedByNameAsync()
        {
            var stacks = await _context.Stacks.AsNoTracking().OrderBy(stack => stack.Name).ToListAsync();

            return stacks;

        }

        public async Task<Stack?> GetByIdAsync(Guid stackId)
        {
            var stack = await _context.Stacks.FirstOrDefaultAsync(stack => stack.Id == stackId);

            return stack;
        }
    }
}
