using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StackForge.Domain.StacksContext.Entities;
using StackForge.Domain.StacksContext.ValueObjects;

namespace StackForge.Infrastructure.Data.Mappings.Stacks
{
    public sealed class StackMap : IEntityTypeConfiguration<Stack>
    {
        public void Configure(EntityTypeBuilder<Stack> builder)
        {
            builder.ToTable("stacks");

            builder.HasKey(x => x.Id)
                .HasName("PK_stacks");

            builder.HasIndex(x => x.Key)
                .IsUnique();

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.Key)
                .HasConversion(key => key.Value,
                value => Key.Create(value))
                .HasColumnName("key")
                .HasMaxLength(20)
                .IsRequired();
            
            builder.Navigation(s => s.Mentors)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}
