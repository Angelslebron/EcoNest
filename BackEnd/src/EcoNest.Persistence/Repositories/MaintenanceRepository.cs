using EcoNest.Application.Interfaces;
using EcoNest.Domain.Entities;
using EcoNest.Domain.Enums;
using EcoNest.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EcoNest.Persistence.Repositories
{
    public class MaintenanceRepository(EcoNestDbContext context) : GenericRepository<Maintenance>(context), IMaintenanceRepository
    {
        public async Task<IEnumerable<Maintenance>> GetByCabinAsync(int cabinId)
            => await _dbSet
                .Include(m => m.cabin)
                .Where(m => m.CabinId == cabinId && m.IsActive)
                .ToListAsync();

        public async Task<IEnumerable<Maintenance>> GetByStatusAsync(MaintenanceStatus status)
            => await _dbSet
                .Include(m => m.cabin)
                .Where(m => m.Status == status && m.IsActive)
                .ToListAsync();
    }
}