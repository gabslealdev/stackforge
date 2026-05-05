using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StackForge.Domain.IdentityContext.Entities;

namespace StackForge.Infrastructure.Data.Mappings.Identity
{
    public sealed class UserRegistrationMap : IEntityTypeConfiguration<UserRegistration>
    {
        public void Configure(EntityTypeBuilder<UserRegistration> builder)
        {
            builder.ToTable("user_registrations");

            builder.HasKey(registration => registration.Id)
                .HasName("PK_user_registrations");

            builder.Property(registration => registration.Id)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(registration => registration.UserId)
                .IsRequired();

            builder.Property(registration => registration.SelectedProfileType)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(registration => registration.Status)
                .HasConversion<int>()
                .IsRequired();
        }

    }
}
