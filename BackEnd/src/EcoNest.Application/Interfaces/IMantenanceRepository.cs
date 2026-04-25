using EcoNest.Application.Core;
using EcoNest.Domain.Entities;
using EcoNest.Domain.Enums;

namespace EcoNest.Application.Interfaces;

public interface IMaintenanceRepository : IGenericRepository<Maintenance>
{
    Task<IEnumerable<Maintenance>> GetByCabinAsync(int cabinId);
    Task<IEnumerable<Maintenance>> GetByStatusAsync(MaintenanceStatus status);
}

