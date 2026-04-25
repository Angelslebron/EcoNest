using EcoNest.Domain.Core;

namespace EcoNest.Domain.Entities
{
    public class Season : BaseEntity
    {
        private static readonly List<Reservation> reservations = [];

        public string Name { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PriceMultiplier { get; set; }

        // Navigation
        public ICollection<Reservation> Reservations { get; set; } = reservations;
    }
}