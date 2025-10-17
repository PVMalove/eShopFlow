namespace Basket.API.Extensions;

public static class CustomServicesExtensions
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCarter();

        services.AddMediatR(config => { config.RegisterServicesFromAssembly(AssemblyReference.Assembly); });

        var connectionString = configuration.GetConnectionString("PgConnection")
                               ?? throw new InvalidOperationException($"Connection string 'PgConnection' not found.");

        services.AddMarten(options =>
            {
                options.Connection(connectionString);
                options.Schema.For<ShoppingCart>().Identity(x => x.AccountName);
            })
            .UseLightweightSessions();

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

        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(options =>
            {
                options.EnableAnnotations();
            });

        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddScoped<ICartRepository, CartRepository>();
        return services;
    }

    public static WebApplication UseCustomServices(this WebApplication application)
    {
        application.MapCarter();
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
                        $"Basket API {description.GroupName.ToUpperInvariant()}");
                }
            });
        }

        return application;
    }
}