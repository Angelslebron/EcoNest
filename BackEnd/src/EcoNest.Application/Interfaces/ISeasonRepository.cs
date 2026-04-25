using EcoNest.Application.Core;
using EcoNest.Domain.Entities;

namespace EcoNest.Application.Interfaces;

public interface ISeasonRepository : IGenericRepository<Season>
{
    Task<Season?> GetActiveSeasonAsync(DateTime date); // returns the season active on that date
    Task<bool> HasOverlapAsync(DateTime startDate, DateTime endDate, int? excludeId = null);
}