using Microsoft.EntityFrameworkCore;
using StackForge.Domain.Identity.Entities;

namespace StackForge.Infrastructure.Data.Context
{
    public class StackForgeDbContext : DbContext
    {
        public StackForgeDbContext(DbContextOptions<StackForgeDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();

        public DbSet<UserRegistration> UserRegistrations => Set<UserRegistration>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StackForgeDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
