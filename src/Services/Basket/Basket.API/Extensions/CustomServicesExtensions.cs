using Basket.API.Services;
using Common.Kernel.Behaviors;
using Common.Kernel.Exceptions.Handler;
using Promotion.GRPS.Protos;

namespace Basket.API.Extensions;

public static class CustomServicesExtensions
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddExceptionHandler<CustomExceptionHandler>();
        services.AddProblemDetails();
        services.AddCarter();

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(AssemblyReference.Assembly);
            config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        });

        services.AddValidatorsFromAssembly(AssemblyReference.Assembly, includeInternalTypes: true);

        var connectionString = configuration.GetConnectionString("PgConnection")
                               ?? throw new InvalidOperationException("Connection string 'PgConnection' not found.");

        services.AddMarten(options =>
            {
                options.Connection(connectionString);
                options.Schema.For<ShoppingCart>().Identity(x => x.AccountName);
            })
            .UseLightweightSessions();

        var redisConnectionString = configuration.GetConnectionString("RedisConnection")
                                    ?? throw new InvalidOperationException("Connection string 'RedisConnection' not found.");

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisConnectionString;
            options.InstanceName = "Basket";
        });

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
            .AddSwaggerGen(options => { options.EnableAnnotations(); });

        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        
        services.AddScoped<ICartRepository, CartRepository>();
        services.Decorate<ICartRepository, RedisCartCachedRepository>();
        
        services.AddScoped<IBasketService, BasketService>();
        
        var promotionGrpcUrl = configuration["GrpcSettings:PromotionServiceUrl"];
        if (string.IsNullOrEmpty(promotionGrpcUrl))
            throw new InvalidOperationException("GrpcSettings:PromotionServiceUrl not found in configuration.");
        
        services.AddGrpcClient<PromotionService.PromotionServiceClient>(option =>
        {
            option.Address = new Uri(promotionGrpcUrl);
        });
        
        return services;
    }

    public static WebApplication UseCustomServices(this WebApplication application)
    {
        application.UseExceptionHandler();

        var apiVersionSet = application.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1, 0))
            .ReportApiVersions()
            .Build();

        application.MapGroup("api/v{version:apiVersion}")
            .WithApiVersionSet(apiVersionSet)
            .WithTags("Basket")
            .MapCarter();

        // if (!application.Environment.IsDevelopment()) 
        //     return application;
        
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

        return application;
    }
}