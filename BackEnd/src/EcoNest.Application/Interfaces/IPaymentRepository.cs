using EcoNest.Application.Core;
using EcoNest.Domain.Entities;

namespace EcoNest.Application.Interfaces;
public interface IPaymentRepository : IGenericRepository<Payment>
{
    Task<IEnumerable<Payment>> GetByReservationAsync(int reservationId);
}