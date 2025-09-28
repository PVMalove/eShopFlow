namespace catalog.API.Extensions;

public static class CustomServicesExtensions
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplication(configuration)
            .AddInfrastructure(configuration);

        services.AddEndpointsApiExplorer()
            .AddSwaggerGen();

        return services;
    }

    public static WebApplication UseCustomServices(this WebApplication application)
    {
        if (application.Environment.IsDevelopment())
        {
            application.UseSwagger()
                .UseSwaggerUI();
        }

        return application;
    }
}