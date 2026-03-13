using StackForge.Application.Shared.Abstractions;
using StackForge.Infrastructure.Data.Context;

namespace StackForge.Infrastructure.Data.UnitOfWork
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly StackForgeDbContext _context;

        public UnitOfWork(StackForgeDbContext context)
        {
            _context = context;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
