using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StackForge.Domain.Profile.Entities;

namespace StackForge.Infrastructure.Data.Mappings.Profile
{
    public sealed class LearnerMap : IEntityTypeConfiguration<LearnerProfile>
    {
        public void Configure(EntityTypeBuilder<LearnerProfile> builder)
        {
            builder.ToTable("learners");

            builder.HasKey(l => l.Id)
                .HasName("PK_learner");

            builder.HasIndex(l => l.UserId)
                .IsUnique();

            builder.OwnsOne(l => l.Name, name =>
            {
                name.Property(n => n.FirstName)
                .HasColumnName("first_name")
                .HasMaxLength(80)
                .IsRequired();

                name.Property(n => n.LastName)
                .HasColumnName("last_name")
                .HasMaxLength(80)
                .IsRequired();
            });

            builder.Property(l => l.UserId)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(l => l.BirthDate)
                .HasColumnType("date")
                .HasColumnName("birth_date")
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasColumnType("timestamp with time zone")
                .HasColumnName("created_at")
                .IsRequired();

        }
    }
}
