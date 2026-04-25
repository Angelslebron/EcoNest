using EcoNest.Application.Core;
using EcoNest.Domain.Entities;

namespace EcoNest.Application.Interfaces;
public interface IObservationRepository : IGenericRepository<Observation>
{
    Task<IEnumerable<Observation>> GetByReservationAsync(int reservationId);
    Task<IEnumerable<Observation>> GetByCabinAsync(int cabinId);
}