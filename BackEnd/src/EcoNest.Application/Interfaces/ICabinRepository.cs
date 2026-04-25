using EcoNest.Application.Core;
using EcoNest.Domain.Entities;
using EcoNest.Domain.Enums;

namespace EcoNest.Application.Interfaces;

using EcoNest.Domain.Entities;

public interface ICabinRepository
{
    Task<IEnumerable<Cabin>> GetAllAsync();
    Task<Cabin?> GetByIdAsync(int id);
    Task<IEnumerable<Cabin>> GetAvailableAsync(DateTime checkIn, DateTime checkOut);
    Task<Cabin> AddAsync(Cabin cabin);
    Task UpdateAsync(Cabin cabin);
}