using EcoNest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoNest.Persistence.Configurations;

public class GuestConfiguration : IEntityTypeConfiguration<Guest>
{
    public void Configure(EntityTypeBuilder<Guest> builder)
    {
        builder.ToTable("Guests");

        builder.HasKey(g => g.Id);

        builder.Property(g => g.Name).IsRequired().HasMaxLength(60);
        builder.Property(g => g.Surname).HasMaxLength(60);
        builder.Property(g => g.Email).HasMaxLength(100);
        builder.Property(g => g.PhoneNumber).HasMaxLength(40);
        builder.Property(g => g.DocumentId).IsRequired().HasMaxLength(20);
        builder.Property(g => g.Address).HasMaxLength(200);
        builder.Property(g => g.Nationality).HasMaxLength(60);

        builder.HasIndex(g => g.DocumentId).IsUnique();
        builder.HasIndex(g => g.Email);

        builder.HasMany(g => g.Reservations)
            .WithOne(r => r.Guest)
            .HasForeignKey(r => r.GuestId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}