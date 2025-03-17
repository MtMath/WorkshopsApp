using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Workshops.Domain.Entities;
using Workshops.Domain.Interfaces;
using Workshops.Infrastructure.Data;
using Workshops.Infrastructure.Identity;
using Workshops.Infrastructure.Repositories;

namespace Workshops.Infrastructure.CrossCutting;

public static class IoC
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContexts(configuration);
        services.AddRepositories();
        services.AddIdentity(configuration);

        return services;
    }

    private static void AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SQLConnection");

        // Application Configuration Section
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString, builder =>
            {
                builder.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "Application");
                builder.EnableRetryOnFailure(3);
            });

            options.UseLazyLoadingProxies();
            options.EnableSensitiveDataLogging();
        });

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

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    }

    private static void AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IdentityConstants.BearerScheme;
                options.DefaultChallengeScheme = IdentityConstants.BearerScheme;
                options.DefaultScheme = IdentityConstants.BearerScheme;
            })
            .AddBearerToken(IdentityConstants.BearerScheme)
            .AddCookie(IdentityConstants.ApplicationScheme);

        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromSeconds(3600);
            options.SlidingExpiration = true;
        });

        services.AddAuthorizationBuilder()
            .AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"))
            .AddPolicy("UserPolicy", policy => policy.RequireRole("Collaborator"));

        services
            .AddIdentityCore<AppUser>()
            .AddRoles<IdentityRole>()
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddApiEndpoints();

        services.AddSingleton(TimeProvider.System);
    }
}