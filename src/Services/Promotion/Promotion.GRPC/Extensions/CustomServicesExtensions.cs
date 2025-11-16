using Promotion.GRPS.Configurations.Mapping;

namespace Promotion.GRPS.Extensions;

public static class CustomServicesExtensions
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionDbString = configuration.GetConnectionString("MySqlConnection")
                                 ?? throw new InvalidOperationException(
                                     "Connection string 'MySqlConnection' not found.");

        services.AddScoped<IDbConnection>(_ => new MySqlConnection(connectionDbString));

        services.AddGrpc();
        services.AddGrpcReflection();

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(AssemblyReference.Assembly);
        });
        
        services.AddScoped<IPromoRepository, PromoRepository>();
        
        Mapping.Configure();
        
        return services;
    }

    public static WebApplication UseCustomServices(this WebApplication application)
    {
        application.MapGrpcService<PromoGrpsService>();
        application.MapGrpcReflectionService();

        return application;
    }
}