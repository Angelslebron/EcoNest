using Microsoft.EntityFrameworkCore;
using EcoNest.Domain.Entities;

namespace EcoNest.Persistence;

public class EcoNestDbContext(DbContextOptions<EcoNestDbContext> options) : DbContext(options)
{
    public DbSet<Cabin> Cabins { get; set; }
    public DbSet<Guest> Guests { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Season> Seasons { get; set; }
    public DbSet<Maintenance> Maintenances { get; set; }
}