using EcoNest.Application.Core;
using EcoNest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoNest.Application.Interfaces
{
    public interface IGuestRepository : IGenericRepository<Guest>
    {
        Task<Guest?> GetByEmailAsync(string Email);
        Task<Guest?> GetByDocumentIdAsync(string DocumentId);
        Task<Guest?> GetWithReservationsAsync(int Id);
        Task<IEnumerable<Guest>> SearchAsync(string Term);

    }
}
