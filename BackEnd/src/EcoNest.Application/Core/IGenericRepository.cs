using EcoNest.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;


namespace EcoNest.Application.Core
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetById(int Id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> ExcistsAsync(int Id);
    }
}
