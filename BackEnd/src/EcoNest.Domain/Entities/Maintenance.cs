using EcoNest.Domain.Core;
using EcoNest.Domain.Enums;

namespace EcoNest.Domain.Entities
{
    public class Maintenance : BaseEntity
    {
        public int CabinId { get; set; }

        public string? Title { get; set; }
        public string Description { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public MaintenanceStatus Status { get; set; }

        // Navigation
        public Cabin cabins {get; set;}
         public Guest Guest { get; set; }
        public Season? Season { get; set; }

        public ICollection<ReservationService> ReservationServices { get; set; } = new List<ReservationService>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public ICollection<Observation> Observations { get; set; } = new List<Observation>();
    }
}