using catalog.Infrastructure.Data.Seed;
using catalog.Infrastructure.Repositories;

namespace catalog.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PgConnection")
                               ?? throw new InvalidOperationException($"Connection string 'PgConnection' not found.");

        services.AddMarten(options => { options.Connection(connectionString); })
            .UseLightweightSessions()
            .InitializeWith<InitializeDatabase>();

        services.AddScoped<IBrandRepository, CatalogRepository>();
        services.AddScoped<ICategoryRepository, CatalogRepository>();
        services.AddScoped<ICatalogItemRepository, CatalogRepository>();

        return services;
    }
}