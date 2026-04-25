using EcoNest.Application.Core;
using EcoNest.Application.Interfaces;
using EcoNest.Domain.Core;
using EcoNest.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EcoNest.Persistence.Repositories;

public class GenericRepository<T>(EcoNestDbContext context) : IGenericRepository<T> where T : BaseEntity
{
    protected readonly EcoNestDbContext _context = context;
    protected readonly DbSet<T> _dbSet = context.Set<T>();

    public virtual async Task<IEnumerable<T>> GetAllAsync()
        => await _dbSet.Where(e => e.IsActive).ToListAsync();

    public virtual async Task<T?> GetByIdAsync(int id)
        => await _dbSet.FirstOrDefaultAsync(e => e.Id == id && e.IsActive);

    public async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        entity.IsActive = false;
        entity.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id)
        => await _dbSet.AnyAsync(e => e.Id == id && e.IsActive);

    public Task<T?> GetById(int Id)
    {
        throw new NotImplementedException();
    }

    Task<T> IGenericRepository<T>.UpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExcistsAsync(int Id)
    {
        throw new NotImplementedException();
    }
}
