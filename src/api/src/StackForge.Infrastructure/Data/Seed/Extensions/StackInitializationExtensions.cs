using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StackForge.Infrastructure.Data.Context;

namespace StackForge.Infrastructure.Data.Seed.Extensions
{
    public static class StackInitializationExtensions
    {
        public static async Task InitializeStackAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;

            var dbContext = services.GetRequiredService<StackForgeDbContext>();
            await dbContext.Database.MigrateAsync();

            var stackSeeder = services.GetRequiredService<StackSeeder>();
            await stackSeeder.SeedAsync();
        }
    }
}
