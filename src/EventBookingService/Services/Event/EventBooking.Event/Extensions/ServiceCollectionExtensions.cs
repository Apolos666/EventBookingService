namespace EventBooking.Event.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, Assembly assembly)
    {
        services.AddCarter();
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        services.AddHttpContextAccessor();
        services.AddScoped<IUserIdentityAccessor, HttpUserIdentityAccessor>();

        services.AddValidatorsFromAssembly(assembly);

        return services;
    }

    public static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(config =>
        {
            config.Configuration = configuration.GetConnectionString("Redis")!;
        });

        return services;
    }

    public static IServiceCollection AddMessageBrokerServices(this IServiceCollection services, IConfiguration configuration, Assembly assembly)
    {
        services.AddMessageBroker(configuration, assembly);

        return services;
    }

    public static IServiceCollection AddBackgroundServices(this IServiceCollection services)
    {
        services.AddHostedService<EventStartDateChecker>();

        return services;
    }
}