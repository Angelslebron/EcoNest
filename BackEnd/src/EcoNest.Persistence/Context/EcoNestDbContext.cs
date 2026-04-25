using EcoNest.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EcoNest.Persistence.Context;

public class EcoNestDbContext(DbContextOptions<EcoNestDbContext> options) : DbContext(options)
{
    public DbSet<Cabin> Cabins => Set<Cabin>();
    public DbSet<Guest> Guests => Set<Guest>();
    public DbSet<Season> Seasons => Set<Season>();
    public DbSet<Reservation> Reservations => Set<Reservation>();
    public DbSet<Maintenance> Maintenances => Set<Maintenance>();
    public DbSet<Domain.Entities.Service> Services => Set<Domain.Entities.Service>();
    public DbSet<ReservationService> ReservationServices => Set<ReservationService>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<Observation> Observations => Set<Observation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EcoNestDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
