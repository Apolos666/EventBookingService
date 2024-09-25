namespace EventBooking.Basket.Extensions;

public static class DataServicesExtensions
{
    public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMarten(config =>
        {
            config.Connection(configuration.GetConnectionString("Database")!);
            config.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate;
            config.Schema.For<EventCart>().Identity(ec => ec.UserId);
        }).UseLightweightSessions();

        return services;
    }
}