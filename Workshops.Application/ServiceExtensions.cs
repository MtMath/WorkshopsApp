using Microsoft.Extensions.DependencyInjection;
using Workshops.Application.Services;

namespace Workshops.Application;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddScoped<CollaboratorService>();
        services.AddScoped<WorkshopsService>();
        
        return services;
    }
}