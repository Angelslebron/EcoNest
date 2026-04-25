using EcoNest.Domain.Core;
using EcoNest.Domain.Enums;
namespace EcoNest.Domain.Entities
{
    public class Reservation : BaseEntity
    {
        public int CabinId { get; set; }
        public int GuestId { get; set; }
        public int? SeasonId { get; set; }

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public ReservationStatus Status { get; set; } = ReservationStatus.Reserved;
        public decimal CostPerNight { get; set; }
        public decimal TotalCost { get; set; }      
        public string? Notes { get; set; }

        // Navigation properties
        public Cabin Cabin { get; set; } = null!;
        public Guest Guest { get; set; } = null!;
        public Season? Season { get; set; } = null!;

        public ICollection<ReservationService> ReservationServices { get; set; } = [];
        public ICollection<Payment> Payments { get; set; } = [];
        public ICollection<Observation> Observations { get; set; } = [];
    }
}