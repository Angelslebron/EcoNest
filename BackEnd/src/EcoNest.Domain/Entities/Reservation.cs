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

        public int Nights { get; set; }

        public decimal CostPerNight { get; set; }
        public decimal TotalCost { get; set; }

        public ReservationStatus Status { get; set; }

        public string? Notes { get; set; }

        // Navigation
        public Cabin Cabin { get; set; }
        public Guest Guest { get; set; }
        public Season? Season { get; set; }

        public ICollection<ReservationService> ReservationServices { get; set; } = new List<ReservationService>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public ICollection<Observation> Observations { get; set; } = new List<Observation>();
    }
}