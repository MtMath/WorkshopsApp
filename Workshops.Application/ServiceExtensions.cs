using Microsoft.Extensions.DependencyInjection;
using Workshops.Application.Services;
using Workshops.Domain.Entities;
using Workshops.Domain.Interfaces;
using Workshops.Infrastructure.Repositories;

namespace Workshops.Application;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddScoped<WorkshopsService>();
        
        return services;
    }
}