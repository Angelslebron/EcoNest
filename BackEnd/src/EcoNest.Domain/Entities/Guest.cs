using EcoNest.Domain.Core;

namespace EcoNest.Domain.Entities
{
    public class Guest : Person
    {
        public string Address { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public string DocumentId { get; set; } = string.Empty;

        //Navigation  properties
        public ICollection<Reservation> Reservations { get; set; } = [];
        
    }
}