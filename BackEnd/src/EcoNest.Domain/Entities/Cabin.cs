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

        //Navigations
        public ICollection<Reservation> Reservations { get; set; } = [];
        public ICollection<Observation> Observations { get; set; } = [];

        public ICollection<Maintenance> Maintenances { get; set; } = [];

    }
}