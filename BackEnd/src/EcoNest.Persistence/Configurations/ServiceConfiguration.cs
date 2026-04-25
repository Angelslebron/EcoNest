using EcoNest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoNest.Persistence.Configurations;

public class ServiceConfiguration : IEntityTypeConfiguration<Domain.Entities.Service>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Service> builder)
    {
        builder.ToTable("Services");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Name).IsRequired().HasMaxLength(60);
        builder.Property(s => s.Description).HasMaxLength(500);
        builder.Property(s => s.Price).IsRequired().HasColumnType("decimal(10,2)");
    }
}