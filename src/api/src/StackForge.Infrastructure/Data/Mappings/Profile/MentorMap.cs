using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StackForge.Domain.ProfileContext.Entities;
using StackForge.Domain.ProfileContext.ValueObjects;
using StackForge.Domain.StacksContext.Entities;

namespace StackForge.Infrastructure.Data.Mappings.Profile
{
    public class MentorMap : IEntityTypeConfiguration<MentorProfile>
    {
        public void Configure(EntityTypeBuilder<MentorProfile> builder)
        {
            builder.ToTable("mentors");

            builder.HasKey(m => m.Id)
                .HasName("PK_mentor");

            builder.HasIndex(m => m.UserId)
                .IsUnique();

            builder.OwnsOne(m => m.Name, name =>
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

            builder.Property(m => m.UserId)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(m => m.BirthDate)
                .HasColumnType("date")
                .HasColumnName("birth_date")
                .IsRequired();

            builder.Property(m => m.CreatedAt)
                .HasColumnType("timestamp with time zone")
                .HasColumnName("created_at")
                .IsRequired();

            builder.OwnsOne(m => m.Education, education => 
            {
                education.Property(e => e.CourseName)
                 .HasColumnName("course_name")
                 .HasMaxLength(100)
                 .IsRequired();

                education.Property(e => e.Institution)
                .HasColumnName("institution")
                .HasMaxLength(100)
                .IsRequired();

                education.Property(e => e.Status)
                .HasColumnName("status")
                .HasConversion<int>()
                .IsRequired();

                education.Property(e => e.ConclusionDate)
                .HasColumnName("conclusion_date")
                .HasColumnType("date")
                .IsRequired();
            });

            builder.Property(x => x.Bio)
                .HasColumnName("bio")
                .HasConversion(
                    bio => bio == null ? null : bio.Value,
                    value => value == null ? null : Bio.Create(value))
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(m => m.Availability)
                .HasColumnName("availability")
                .HasConversion<int>()
                .IsRequired();

            builder
                .HasMany(m => m.Stacks)
                .WithMany(s => s.Mentors)
                .UsingEntity(
                    "MentorProfileStack",
                    right => right.HasOne(typeof(Stack)).WithMany().HasForeignKey("stack_id"),
                    left => left.HasOne(typeof(MentorProfile)).WithMany().HasForeignKey("mentor_id"),
                    join =>
                    {
                        join.ToTable("mentor_profile_stack");
                        join.HasKey("mentor_id", "stack_id");
                    });
            
            builder.Navigation(m => m.Stacks)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
