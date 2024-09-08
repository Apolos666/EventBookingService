namespace Booking.API;

public static class DependencyInjections
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCarter();
        
        services.AddHttpContextAccessor();
        services.AddScoped<IUserIdentityAccessor, HttpUserIdentityAccessor>();

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.MapCarter();

        return app;
    }
}