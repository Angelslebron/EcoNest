using EcoNest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoNest.Persistence.Configurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.ToTable("Reservations");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.CheckInDate).IsRequired();
        builder.Property(r => r.CheckOutDate).IsRequired();

        builder.Property(r => r.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(r => r.CostPerNight)
            .IsRequired()
            .HasColumnType("decimal(10,2)");

        builder.Property(r => r.TotalCost)
            .IsRequired()
            .HasColumnType("decimal(10,2)");

        builder.HasOne(r => r.Season)
            .WithMany(s => s.Reservations)
            .HasForeignKey(r => r.SeasonId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(r => r.Observations)
            .WithOne(o => o.Reservation)
            .HasForeignKey(o => o.ReservationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(r => r.ReservationServices)
            .WithOne(rs => rs.Reservation)
            .HasForeignKey(rs => rs.ReservationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(r => r.Payments)
            .WithOne(p => p.Reservation)
            .HasForeignKey(p => p.ReservationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}