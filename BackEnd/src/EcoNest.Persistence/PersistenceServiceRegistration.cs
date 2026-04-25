using EcoNest.Application.Interfaces;
using EcoNest.Persistence.Context;
using EcoNest.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EcoNest.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EcoNestDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<ICabinRepository, CabinRepository>();
        services.AddScoped<IGuestRepository, GuestRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
        services.AddScoped<ISeasonRepository, SeasonRepository>();
        services.AddScoped<IMaintenanceRepository, MaintenanceRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IObservationRepository, ObservationRepository>();

        return services;
    }
}