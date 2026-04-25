using EcoNest.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EcoNest.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<CabinService>();
        services.AddScoped<GuestService>();
        services.AddScoped<ReservationService>();
        services.AddScoped<SeasonService>();
        services.AddScoped<MaintenanceService>();
        services.AddScoped<ServiceService>();

        return services;
    }
}
