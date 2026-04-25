using EcoNest.Application.Core;
using EcoNest.Application.Interfaces;
using EcoNest.Domain.Core;
using EcoNest.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EcoNest.Persistence.Repositories;

public class GenericRepository<T>(EcoNestDbContext context)
    : GenericRepositoryBase<T>(context), IGenericRepository<T> 
    where T : BaseEntity
{
    protected readonly EcoNestDbContext _context = context;
    protected readonly DbSet<T> _dbSet = context.Set<T>();

    public virtual async Task<IEnumerable<T>> GetAllAsync()
        => await _dbSet.Where(e => e.IsActive).ToListAsync();

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

    Task<T> IGenericRepository<T>.UpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }

 
}
