using EcoNest.Domain.Core;
using EcoNest.Domain.Enums;

namespace EcoNest.Domain.Entities
{
    public class Cabin : BaseEntity
    {
        public string Name {get; set;} = string.Empty;
        public string? Description {get; set;}
        public string Location {get; set;} = string.Empty;
        public int Capacity {get;set;}
        public decimal BasePricePerDays { get; set; } 
        public CabinStatus State { get; set; } = CabinStatus.Available;

        //Navegation
        public ICollection<Reservation> Reservations {get; set;} = new List<Reservation>();
        public ICollection<Observation> Observations {get; set;} = new List<Observation>();
        public ICollection<Maintenance> Maintenances {get;set;} = new List<Maintenance>();

    }
}