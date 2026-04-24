using EcoNest.Domain.Core;

namespace EcoNest.Domain.Entities
{
    public class Season : BaseEntity
    {
        public string? Name { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public decimal PriceMultiplier { get; set; }

        // Navigation
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}