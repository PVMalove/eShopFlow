using System.Data;
using MySqlConnector;
using Promotion.GRPS.Services;

namespace Promotion.GRPS.Extensions;

public static class CustomServicesExtensions
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpc();

        var connectionDbString = configuration.GetConnectionString("MySqlConnection")
                                 ?? throw new InvalidOperationException(
                                     "Connection string 'MySqlConnection' not found.");

        services.AddScoped<IDbConnection>(_ => new MySqlConnection(connectionDbString));
        
        services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        services.AddMediatR(config => { config.RegisterServicesFromAssembly(AssemblyReference.Assembly); });

        return services;
    }

    public static WebApplication UseCustomServices(this WebApplication application)
    {
        if (!application.Environment.IsDevelopment())
            return application;
        
        application.MapGrpcService<GreeterService>();
        return application;
    }
}