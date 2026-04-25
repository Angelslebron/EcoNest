using EcoNest.Domain.Core;

namespace EcoNest.Domain.Entities
{
    public class Service : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }

        //Navigation Properties
        public ICollection<ReservationService> ReservationServices { get; set; } = [];
    }
}