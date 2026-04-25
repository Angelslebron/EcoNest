using EcoNest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoNest.Persistence.Configurations;

public class ReservationServiceConfiguration : IEntityTypeConfiguration<ReservationService>
{
    public void Configure(EntityTypeBuilder<ReservationService> builder)
    {
        builder.ToTable("ReservationServices");
        builder.HasKey(rs => rs.Id);
        builder.Property(rs => rs.Quantity).IsRequired();

        builder.HasOne(rs => rs.Service)
            .WithMany(s => s.ReservationServices)
            .HasForeignKey(rs => rs.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}