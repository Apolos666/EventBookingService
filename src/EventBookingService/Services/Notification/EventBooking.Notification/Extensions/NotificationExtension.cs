namespace EventBooking.Notification.Extensions;

public static class NotificationExtension
{
    public static IServiceCollection AddNotificationServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCarter();
        services.AddSignalR();
        
        // Authentication and Authorization services
        services.AddAuthentication("web_app")
            .AddJwtBearer("web_app", options =>
            {
                options.Authority = configuration["Keycloak:Authority"];
                options.MetadataAddress = configuration["Keycloak:MetadataAddress"];
                options.RequireHttpsMetadata = configuration.GetValue<bool>("Keycloak:RequireHttpsMetadata");
                options.Audience = configuration["Keycloak:Audience"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.Name,
                    RoleClaimType = ClaimTypes.Role,
                    ValidateIssuer = false,
                    ValidIssuers = configuration.GetValue<IEnumerable<string>>("Keycloak:ValidIssuers"),
                    ValidateAudience = true,
                    ValidAudiences = configuration.GetValue<IEnumerable<string>>("Keycloak:ValidAudiences")
                };
            });

        services
            .AddAuthorization();
        
        services.AddCors();            
        
        // Asynchronous messaging services
        services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());
        
        return services;
    }
    
    public static WebApplication MapCustomHubs(this WebApplication app)
    {
        app.MapHub<NotificationHub>("/notifications").RequireAuthorization();

        return app;
    }
}