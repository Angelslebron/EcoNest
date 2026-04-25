using EcoNest.Domain.Entities;
using EcoNest.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoNest.Application.Interfaces
{
    public interface IReservationRepository
    {
        Task<Reservation?> GetByGuestAsync(int GuestId);
        Task<IEnumerable<Reservation>> GetByCabinAsync(int CabinId);
        Task<IEnumerable<Reservation>> GetByStatusAsync(ReservationStatus Status);
        Task<bool> HasConflictAsync(int CabinId, DateTime CheckIn, DateTime CheckOut, int? ExcludeId = null);


    }
}
