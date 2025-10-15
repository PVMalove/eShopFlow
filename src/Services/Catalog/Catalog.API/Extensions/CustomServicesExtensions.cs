using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using catalog.Application;
using catalog.Infrastructure;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace catalog.API.Extensions;

public static class CustomServicesExtensions
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplication(configuration)
            .AddInfrastructure(configuration);

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

        services.AddControllers();

        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();
        
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        
        return services;
    }

    public static WebApplication UseCustomServices(this WebApplication application)
    {
        application.MapControllers();

        if (application.Environment.IsDevelopment())
        {
            application.UseSwagger();
            application.UseSwaggerUI(options =>
            {
                var provider = application.Services.GetRequiredService<IApiVersionDescriptionProvider>();
                
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        $"Catalog API {description.GroupName.ToUpperInvariant()}");
                }
            });
        }

        return application;
    }
}