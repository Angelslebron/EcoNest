using EcoNest.Domain.Core;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EcoNest.Persistence.Repositories
{
    public class GenericRepositoryBase<T> where T : BaseEntity
    {
        
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        
        public GenericRepositoryBase(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        
        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Id == id && e.IsActive);
        }
    }
}