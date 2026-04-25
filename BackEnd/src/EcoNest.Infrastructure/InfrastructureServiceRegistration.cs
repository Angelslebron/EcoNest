using EcoNest.Infrastructure.Email;
using EcoNest.Infrastructure.Files;
using Microsoft.Extensions.DependencyInjection;

namespace EcoNest.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IFileService, FileService>();
        return services;
    }
}

