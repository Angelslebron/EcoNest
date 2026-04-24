using EcoNest.Domain.Core;

namespace EcoNest.Domain.Entities
{
    public class ReservationService : BaseEntity
    {
        public int ReservationId { get; set; }
        public int ServiceId { get; set; }

        public int Quantity { get; set; }

        // Navigation
        public Reservation Reservation { get; set; }
        public Service Service { get; set; }
    }
}