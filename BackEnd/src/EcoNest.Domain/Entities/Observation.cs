using EcoNest.Domain.Core;

namespace EcoNest.Domain.Entities
{
    public class Observation : BaseEntity
    {
        public int ReservationId { get; set; }

        public string Comment { get; set; } = string.Empty;

        // Navigation
        public Reservation Reservation { get; set; }
    }
}