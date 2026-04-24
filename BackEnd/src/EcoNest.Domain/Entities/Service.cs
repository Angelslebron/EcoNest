using EcoNest.Domain.Core;

namespace EcoNest.Domain.Entities
{
    public class Service : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public decimal Price { get; set; }

        public ICollection<ReservationService> ReservationServices { get; set; } = new List<ReservationService>();
    }
}