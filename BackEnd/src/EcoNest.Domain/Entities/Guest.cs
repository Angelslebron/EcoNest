using EcoNest.Domain.Core;

namespace EcoNest.Domain.Entities
{
    public class Guest : Person
    {
        public string Address { get; set; } = string.Empty;
        public string Nacionality { get; set; } = string.Empty;
        public string? DocumentNumber {get;set;}

        //Navigation  properties
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

        //Navigation
        public ICollection<Reservation> reservations {get; set;} = new List<Reservation>();
    }
}