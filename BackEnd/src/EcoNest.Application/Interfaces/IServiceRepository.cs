using EcoNest.Application.Core;
using EcoNest.Domain.Entities;

namespace EcoNest.Application.Interfaces;

public interface IServiceRepository : IGenericRepository<Service>
{
    Task<IEnumerable<Service>> GetByReservationAsync(int reservationId);
}
