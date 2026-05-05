using StackForge.Infrastructure.Data.Context;

namespace StackForge.Infrastructure.Data.Seed
{
    public sealed class StackSeeder
    {
        private readonly StackForgeDbContext _context;

        public StackSeeder(StackForgeDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            var isInStacks = _context.Stacks.Select(stack => stack.Key);

            var stacks = StackSeedData.GetStacks().Where(stack => !isInStacks.Contains(stack.Key)).ToList();

            await _context.Stacks.AddRangeAsync(stacks);
            await _context.SaveChangesAsync();
        }
    }
}
 