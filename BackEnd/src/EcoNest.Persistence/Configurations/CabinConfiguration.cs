using EcoNest.Domain.Entities;
using EcoNest.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoNest.Persistence.Configurations;

public class CabinConfiguration : IEntityTypeConfiguration<Cabin>
{
    public void Configure(EntityTypeBuilder<Cabin> builder)
    {
        builder.ToTable("Cabins");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(60);

        builder.Property(c => c.Location)
            .HasMaxLength(100);

        builder.Property(c => c.Capacity)
            .IsRequired();

        builder.Property(c => c.Description)
            .HasMaxLength(500);

        builder.Property(c => c.State)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(c => c.CreatedAt).IsRequired();
        builder.Property(c => c.IsActive).IsRequired();

        builder.HasMany(c => c.Reservations)
            .WithOne(r => r.Cabin)
            .HasForeignKey(r => r.CabinId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.Maintenances)
            .WithOne(m => m.Cabin)
            .HasForeignKey(m => m.CabinId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Observations)
            .WithOne(o => o.Cabin)
            .HasForeignKey(o => o.CabinId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}

