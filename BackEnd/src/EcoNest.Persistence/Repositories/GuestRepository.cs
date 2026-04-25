using EcoNest.Application.Interfaces;
using EcoNest.Domain.Entities;
using EcoNest.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EcoNest.Persistence.Repositories;

public class GuestRepository(EcoNestDbContext context) : GenericRepository<Guest>(context), IGuestRepository
{
    public async Task<Guest?> GetByEmailAsync(string email)
        => await _dbSet.FirstOrDefaultAsync(g => g.Email == email && g.IsActive);

    public async Task<Guest?> GetByCedulaAsync(string cedula)
        => await _dbSet.FirstOrDefaultAsync(g => g.DocumentId == cedula && g.IsActive);

    public async Task<Guest?> GetWithReservationsAsync(int id)
        => await _dbSet
            .Include(g => g.Reservations).ThenInclude(r => r.Cabin)
            .Include(g => g.Reservations).ThenInclude(r => r.Season)
            .FirstOrDefaultAsync(g => g.Id == id && g.IsActive);

    public async Task<IEnumerable<Guest>> SearchAsync(string term)
    {
        var lower = term.ToLower();
        return await _dbSet
            .Where(g => g.IsActive && (
                g.Name.Contains(lower, StringComparison.CurrentCultureIgnoreCase) ||
                g.Surname.Contains(lower, StringComparison.CurrentCultureIgnoreCase) ||
                g.Email.Contains(lower, StringComparison.CurrentCultureIgnoreCase) ||
                g.DocumentId.Contains(term)))
            .ToListAsync();
    }

    public Task<Guest?> GetByDocumentIdAsync(string documentId)
    {
        throw new NotImplementedException();
    }
}
