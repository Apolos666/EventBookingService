namespace EventBooking.Notification.Extensions;

public static class NotificationExtension
{
    public static IServiceCollection AddNotificationServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCarter();
        services.AddSignalR();
        
        // Authentication and Authorization services
        services.AddKeycloakWebApiAuthentication(configuration);

        services
            .AddAuthorization()
            .AddKeycloakAuthorization()
            .AddAuthorizationBuilder()
            .AddCustomAuthorizationPolicies();
        
        services.AddCors();            
        
        // Asynchronous messaging services
        services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());
        
        return services;
    }
    
    public static WebApplication MapCustomHubs(this WebApplication app)
    {
        app.MapHub<NotificationHub>("/notifications");

        return app;
    }
}