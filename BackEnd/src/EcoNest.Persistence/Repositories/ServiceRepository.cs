using EcoNest.Application.Interfaces;
using EcoNest.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoNest.Persistence.Repositories;
public class ServiceRepository(EcoNestDbContext context) : GenericRepository<Domain.Entities.Service>(context), IServiceRepository
{
    public async Task<IEnumerable<Domain.Entities.Service>> GetByReservationAsync(int reservationId)
        => await _context.ReservationServices
            .Where(rs => rs.ReservationId == reservationId)
            .Include(rs => rs.Service)
            .Select(rs => rs.Service)
            .ToListAsync();
}
