using Microsoft.EntityFrameworkCore;
using StackForge.Domain.Identity.Entities;
using StackForge.Domain.Profile.Entities;
using StackForge.Domain.Stacks.Entities;

namespace StackForge.Infrastructure.Data.Context
{
    public class StackForgeDbContext : DbContext
    {
        public StackForgeDbContext(DbContextOptions<StackForgeDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<UserRegistration> UserRegistrations => Set<UserRegistration>();
        public DbSet<LearnerProfile> Learners => Set<LearnerProfile>();
        public DbSet<MentorProfile> Mentors => Set<MentorProfile>();
        public DbSet<Stack> Stacks => Set<Stack>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StackForgeDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
