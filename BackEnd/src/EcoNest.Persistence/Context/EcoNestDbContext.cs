using Microsoft.EntityFrameworkCore;
using EcoNest.Domain.Entities;

namespace EcoNest.Persistence.Context
{

    public class EcoNestDbContext : DbContext
    {
        public EcoNestDbContext(DbContextOptions<EcoNestDbContext> options)
        : base(options)
        {
        }

        public DbSet<Cabin> Cabins {get;set;}
        public DbSet<Guest> Guests {get;set;}
        public DbSet<Reservation> Reservations {get; set;}
        public DbSet<Season> Seasons {get;set;}
        public DbSet<Maintenance> Maintenances {get; set;}
        public DbSet<Service> Services {get; set;}
        public DbSet<ReservationService> ReservationServices {get; set;}
        public DbSet<Payment> Payments {get; set;}
        public DbSet<Observation> Observations {get; set;}
    }
}