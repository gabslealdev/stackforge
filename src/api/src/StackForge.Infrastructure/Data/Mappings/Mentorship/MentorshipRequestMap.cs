using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StackForge.Domain.MentorshipContext.Entities;
using StackForge.Domain.MentorshipContext.ValueObjects;
using StackForge.Domain.ProfileContext.Entities;
using StackForge.Domain.StacksContext.Entities;

namespace StackForge.Infrastructure.Data.Mappings.Mentorship;

public sealed class MentorshipRequestMap : IEntityTypeConfiguration<MentorshipRequest>
{
    public void Configure(EntityTypeBuilder<MentorshipRequest> builder)
    {
        builder.ToTable("mentorship_requests");
        
        builder.HasKey(x => x.Id).HasName("PK_mentorship_requests");

        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.LearnerId)
            .HasColumnName("learner_id")
            .IsRequired();

        builder.Property(x => x.MentorId)
            .HasColumnName("mentor_id") 
            .IsRequired();
        
        builder.Property(x => x.StackId)
            .HasColumnName("stack_id")
            .IsRequired();
        
        builder.Property(x => x.Goal)
            .HasConversion(
                goal => goal.Value,
                value => Goal.Create(value))
            .HasColumnName("goal")
            .HasMaxLength(150)
            .IsRequired();
        
        builder.Property(x => x.CreatedAt)
            .HasColumnType("timestamp with time zone")
            .HasColumnName("created_at")
            .IsRequired();
        
        builder.Property(x => x.Status)
            .HasColumnName("status")
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.DecidedAt)
            .HasColumnName("decided_at")
            .HasColumnType("timestamp with time zone");
        
        builder.HasOne<LearnerProfile>()
            .WithMany()
            .HasForeignKey(x => x.LearnerId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne<MentorProfile>()
            .WithMany()
            .HasForeignKey(x => x.MentorId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne<Stack>()
            .WithMany()
            .HasForeignKey(x => x.StackId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}