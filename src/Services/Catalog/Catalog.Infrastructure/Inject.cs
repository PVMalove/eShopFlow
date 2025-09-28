using catalog.Infrastructure.Data.Seed;
using Marten;

namespace catalog.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PgConnection")
            ?? throw new InvalidOperationException($"Connection string 'PgConnection' not found.");

        services.AddMarten(options =>
        {
            options.Connection(connectionString);
        })
        .UseLightweightSessions()
        .InitializeWith<InitializeDatabase>();

        return services;
    }
}