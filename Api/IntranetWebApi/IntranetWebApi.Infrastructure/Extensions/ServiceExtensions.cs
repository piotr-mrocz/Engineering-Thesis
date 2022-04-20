using IntranetWebApi.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace IntranetWebApi.Infrastructure.Extensions;

public static class ServiceExtensions
{
    public static void AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddScoped<DatabaseSeeder>();
    }

    public static void SeedDatabase(this IServiceScope scope)
    {
        var databaseSeeder = scope.ServiceProvider.GetService<DatabaseSeeder>();
        databaseSeeder.Seed();
    }
}
