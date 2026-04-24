using EcoNest.Domain.Core;

namespace EcoNest.Domain.Entities
{
    public class Guest : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string? Email {get; set;} = string.Empty;
        public string? PhoneNumber {get;set;}
        public string? DocumentNumber {get;set;}

        //Navigation
        public ICollection<Reservation> Reservations {get; set;} = new List<Reservation>();
    }
}