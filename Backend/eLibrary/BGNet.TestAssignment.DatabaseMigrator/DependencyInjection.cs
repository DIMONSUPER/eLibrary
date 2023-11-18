using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BGNet.TestAssignment.DatabaseMigrator;

public static class DependencyInjection
{
    public static IServiceCollection AddMigrations<T>(this IServiceCollection services, string connectionString) where T : DbContext
    {
        services.AddDbContext<T>(opt => opt.UseNpgsql(connectionString));

        try 
        {
                
        }
        catch (Exception ex) 
        {

        }

        return services;
    }
}
