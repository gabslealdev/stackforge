using Microsoft.EntityFrameworkCore;
using StackForge.Application.StackContext;
using StackForge.Domain.StacksContext.Entities;
using StackForge.Infrastructure.Data.Context;

namespace StackForge.Infrastructure.Data.Repositories.StackContext
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

        public async Task<IReadOnlyList<Stack>> SearchByTermAsync(string term)
        {
            if (string.IsNullOrEmpty(term))
                return [];
            
            var normalizeTerm = term.Trim().ToLower();

            return await _context.Stacks
                .AsNoTracking()
                .Where(stack =>
                    stack.Name.ToLower().Contains(normalizeTerm))
                .OrderBy(stack => stack.Name)
                .Take(10)
                .ToListAsync();
        }
    }
}
