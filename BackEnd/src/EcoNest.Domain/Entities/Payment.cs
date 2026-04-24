using EcoNest.Domain.Core;
using EcoNest.Domain.Enums;

namespace EcoNest.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public int ReservationId { get; set; }

        public DateTime PaymentDate { get; set; }

        public decimal Amount { get; set; }

        public PaymentStatus Status { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;

        // Navigation
        public Reservation Reservation { get; set; } 
    }
}