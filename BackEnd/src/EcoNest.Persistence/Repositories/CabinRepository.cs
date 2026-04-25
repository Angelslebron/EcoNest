using EcoNest.Application.Interfaces;
using EcoNest.Domain.Entities;
using EcoNest.Domain.Enums;
using EcoNest.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EcoNest.Persistence.Repositories;

public class CabinRepository(EcoNestDbContext context) : GenericRepository<Cabin>(context), ICabinRepository
{
    public async Task<IEnumerable<Cabin>> GetByStatusAsync(CabinStatus status)
        => await _dbSet
            .Where(c => c.State == status && c.IsActive)
            .ToListAsync();

    public async Task<IEnumerable<Cabin>> GetAvailableAsync(DateTime checkIn, DateTime checkOut)
        => await _dbSet
            .Where(c => c.IsActive
                && c.State == CabinStatus.Available
                && !c.Reservations.Any(r =>
                    r.Status != ReservationStatus.Cancelled
                    && r.CheckInDate < checkOut
                    && r.CheckOutDate > checkIn))
            .ToListAsync();

    public async Task<Cabin?> GetWithDetailsAsync(int id)
        => await _dbSet
            .Include(c => c.Reservations).ThenInclude(r => r.Guest)
            .Include(c => c.Maintenances)
            .Include(c => c.Observations)
            .FirstOrDefaultAsync(c => c.Id == id && c.IsActive);
}
