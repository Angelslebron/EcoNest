using EcoNest.Application.Interfaces;
using EcoNest.Domain.Entities;
using EcoNest.Domain.Enums;
using EcoNest.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EcoNest.Persistence.Repositories;

// ── SeasonRepository ──────────────────────────────────────────────────────────
public class SeasonRepository(EcoNestDbContext context) : GenericRepository<Season>(context), ISeasonRepository
{
    public async Task<Season?> GetActiveSeasonAsync(DateTime date)
        => await _dbSet.FirstOrDefaultAsync(s =>
            s.IsActive && s.StartDate <= date && s.EndDate >= date);

    public async Task<bool> HasOverlapAsync(DateTime startDate, DateTime endDate, int? excludeId = null)
        => await _dbSet.AnyAsync(s =>
            s.IsActive
            && (excludeId == null || s.Id != excludeId)
            && s.StartDate < endDate
            && s.EndDate > startDate);
}

