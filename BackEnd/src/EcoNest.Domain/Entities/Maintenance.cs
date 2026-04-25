using EcoNest.Domain.Core;
using EcoNest.Domain.Enums;

namespace EcoNest.Domain.Entities
{
    public class Maintenance : BaseEntity
    {
        public int CabinId { get; set; }

        public string? Title { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public MaintenanceStatus Status { get; set; }

        // Navigation properties
        public Cabin cabin { get; set; } = null!;
        
    }
}