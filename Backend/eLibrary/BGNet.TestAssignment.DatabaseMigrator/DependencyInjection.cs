using BGNet.TestAssignment.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BGNet.TestAssignment.DatabaseMigrator;

public static class DependencyInjection
{
    public static IServiceCollection AddMigrations(this IServiceCollection services, string? connectionString)
    {
        services.AddDbContext<LibraryDbContext>(opt => opt.UseNpgsql(connectionString));
        using var serviceProvider = services.BuildServiceProvider();

        try
        {
            var applicationContext = serviceProvider.GetRequiredService<LibraryDbContext>();

            applicationContext.Database.Migrate();
        }
        catch (Exception ex) 
        {
            System.Diagnostics.Debug.WriteLine($"{nameof(AddMigrations)}: {ex.Message}");
        }

        return services;
    }
}
