using EcoNest.Application;
using EcoNest.Application.Services;
using EcoNest.Infrastructure;
using EcoNest.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EcoNest.API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddAllLayers(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationServices();
        services.AddPersistenceServices(configuration);
        services.AddInfrastructureServices();
        return services;
    }
}
