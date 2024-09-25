namespace EventBooking.Event.Extensions;

public static class DataServicesExtensions
{
    public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMarten(config =>
        {
            config.Connection(configuration.GetConnectionString("Database")!);
            config.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate;
        }).UseLightweightSessions();

        services.AddScoped<IEventRepository, EventRepository>();
        services.Decorate<IEventRepository, CachedEventRepository>();

        return services;
    }
}