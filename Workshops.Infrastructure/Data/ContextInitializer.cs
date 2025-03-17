using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Workshops.Infrastructure.Data;

public static class ContextInitializer
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var contextInitialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await contextInitialiser.InitialiseAsync();
    }

    public class ApplicationDbContextInitialiser(
        ILogger<ApplicationDbContextInitialiser> logger,
        AppDbContext context,
        AppIdentityDbContext identityContext)
    {
        public async Task InitialiseAsync()
        {
            await MigrateIdentityContext();
            await MigrateAppContext();
        }
        
        private async Task MigrateIdentityContext()
        {
            try
            {
                await identityContext.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while initialising the identity database.");
                throw;
            }
        }

        private async Task MigrateAppContext()
        {
            try
            {
                await context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while initialising the app database.");
                throw;
            }
        }
    }
}