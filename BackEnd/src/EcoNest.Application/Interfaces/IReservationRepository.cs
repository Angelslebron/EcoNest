using EcoNest.Application.Core;
using EcoNest.Domain.Entities;
using EcoNest.Domain.Enums;

namespace EcoNest.Application.Interfaces;

public interface IReservationRepository : IGenericRepository<Reservation>
{
    Task<Reservation?> GetWithDetailsAsync(int id);
    Task<IEnumerable<Reservation>> GetByGuest(int guestId);
    Task<IEnumerable<Reservation>> GetByCabinAsync(int cabinId);
    Task<IEnumerable<Reservation>> GetByStatusAsync(ReservationStatus status);
    Task<IEnumerable<Reservation>> GetActiveAndUpcomingAsync();
    Task<bool> HasConflictAsync(int cabinId, DateTime checkIn, DateTime checkOut, int? excludeId = null);    
}
