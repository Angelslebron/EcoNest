using EcoNest.Application.Interfaces;
using EcoNest.Domain.Entities;
using EcoNest.Domain.Enums;
using EcoNest.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EcoNest.Persistence.Repositories;

public class ReservationRepository(EcoNestDbContext context) : GenericRepository<Reservation>(context), IReservationRepository
{
    public async Task<Reservation?> GetWithDetailsAsync(int id)
        => await _dbSet
            .Include(r => r.Cabin)
            .Include(r => r.Guest)
            .Include(r => r.Season)
            .Include(r => r.Observations)
            .Include(r => r.ReservationServices).ThenInclude(rs => rs.Service)
            .Include(r => r.Payments)
            .FirstOrDefaultAsync(r => r.Id == id && r.IsActive);

    public async Task<IEnumerable<Reservation>> GetByGuestAsync(int guestId)
        => await _dbSet
            .Include(r => r.Cabin)
            .Include(r => r.Season)
            .Where(r => r.GuestId == guestId && r.IsActive)
            .ToListAsync();

    public async Task<IEnumerable<Reservation>> GetByCabinAsync(int cabinId)
        => await _dbSet
            .Include(r => r.Guest)
            .Include(r => r.Season)
            .Where(r => r.CabinId == cabinId && r.IsActive)
            .ToListAsync();

    public async Task<IEnumerable<Reservation>> GetByStatusAsync(ReservationStatus status)
        => await _dbSet
            .Include(r => r.Cabin)
            .Include(r => r.Guest)
            .Where(r => r.Status == status && r.IsActive)
            .ToListAsync();

    public async Task<IEnumerable<Reservation>> GetActiveAndUpcomingAsync()
        => await _dbSet
            .Include(r => r.Cabin)
            .Include(r => r.Guest)
            .Include(r => r.Season)
            .Where(r => r.IsActive
                && (r.Status == ReservationStatus.Reserved || r.Status == ReservationStatus.Confirmed)
                && r.CheckOutDate >= DateTime.UtcNow)
            .OrderBy(r => r.CheckInDate)
            .ToListAsync();

    public async Task<bool> HasConflictAsync(int cabinId, DateTime checkIn, DateTime checkOut, int? excludeId = null)
        => await _dbSet.AnyAsync(r =>
            r.CabinId == cabinId
            && r.IsActive
            && r.Status != ReservationStatus.Cancelled
            && (excludeId == null || r.Id != excludeId)
            && r.CheckInDate < checkOut
            && r.CheckOutDate > checkIn);

    Task<Reservation?> IReservationRepository.GetByGuestAsync(int GuestId)
    {
        throw new NotImplementedException();
    }
}
