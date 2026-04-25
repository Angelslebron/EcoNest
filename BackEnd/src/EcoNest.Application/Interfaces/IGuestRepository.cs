using EcoNest.Domain.Entities;

namespace EcoNest.Application.Interfaces;

public interface IGuestRepository
{
    Task<IEnumerable<Guest>> GetAllAsync();
    Task<Guest?> GetByIdAsync(int id);
    Task<IEnumerable<Guest>> SearchAsync(string term);
    Task<Guest?> GetByDocumentIdAsync(string documentId);
    Task<Guest> AddAsync(Guest guest);
    Task UpdateAsync(Guest guest);
    Task DeleteAsync(Guest guest);
}