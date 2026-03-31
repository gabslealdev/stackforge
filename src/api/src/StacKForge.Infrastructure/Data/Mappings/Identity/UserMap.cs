using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StackForge.Domain.Identity.Entities;
using StackForge.Domain.Identity.ValueObjects;


namespace StackForge.Infrastructure.Data.Mappings.Identity
{
    public sealed class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(user => user.Id)
                .HasName("PK_users");

            builder.Property(user => user.Id)
                .ValueGeneratedNever();

            builder.Property(user => user.Email)
                .HasConversion(
                    email => email.Value,
                    value => Email.Create(value))
                .HasColumnName("email")
                .HasMaxLength(254)
                .IsRequired();

            builder.Property(user => user.PasswordHash)
                .HasConversion(
                    passwordHash => passwordHash.Value,
                    value => PasswordHash.Create(value))
                .HasColumnName("password_hash")
                .HasMaxLength(1000)
                .IsRequired();

            builder.HasIndex(user => user.Id)
                .IsUnique();

        }
    }
}
