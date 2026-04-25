using EcoNest.Application.Interfaces;
using EcoNest.Domain.Entities;
using EcoNest.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoNest.Persistence.Repositories;
public class ObservationRepository(EcoNestDbContext context) : GenericRepository<Observation>(context), IObservationRepository
{
    public async Task<IEnumerable<Observation>> GetByReservationAsync(int reservationId)
        => await _dbSet
            .Where(o => o.ReservationId == reservationId && o.IsActive)
            .ToListAsync();

    public async Task<IEnumerable<Observation>> GetByCabinAsync(int cabinId)
        => await _dbSet
            .Where(o => o.CabinId == cabinId && o.IsActive)
            .ToListAsync();
}
