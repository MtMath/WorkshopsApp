using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Workshops.Infrastructure.Data;
using Workshops.Infrastructure.Identity;
using Workshops.Infrastructure.Repositories;

namespace Workshops.Infrastructure.CrossCutting;

public static class IoC
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContexts(configuration);
        services.AddRepositories();
        services.AddIdentity();

        return services;
    }

    private static void AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SQLConnection");
        
        
        // Identity Configuration Section
        services.AddDbContext<AppIdentityDbContext>(options =>
        {
            options.UseSqlServer(connectionString, builder =>
            {
                builder.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "Identity");
                builder.EnableRetryOnFailure(3);
            });
            
            options.EnableSensitiveDataLogging();
        });
    }
    
}