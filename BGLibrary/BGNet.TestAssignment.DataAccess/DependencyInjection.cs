using BGNet.TestAssignment.DataAccess.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace BGNet.TestAssignment.DataAccess;

public static class DependencyInjection
{
    public static IServiceCollection RegisterRepository(this IServiceCollection services)
    {
        services.AddScoped<IRepository, Repository.Repository>();

        return services;
    }
}
