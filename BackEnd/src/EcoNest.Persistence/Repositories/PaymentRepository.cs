using EcoNest.Application.Interfaces;
using EcoNest.Domain.Entities;
using EcoNest.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoNest.Persistence.Repositories;
public class PaymentRepository(EcoNestDbContext context) : GenericRepository<Payment>(context), IPaymentRepository
{
    public async Task<IEnumerable<Payment>> GetByReservationAsync(int reservationId)
        => await _dbSet
            .Where(p => p.ReservationId == reservationId && p.IsActive)
            .ToListAsync();
}
