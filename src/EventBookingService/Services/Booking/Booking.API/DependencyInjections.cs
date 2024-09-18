namespace Booking.API;

public static class DependencyInjections
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCarter();
        
        services.AddHttpContextAccessor();
        services.AddScoped<IUserIdentityAccessor, HttpUserIdentityAccessor>();
        
        services.AddAuthentication()
            .AddJwtBearer("webapp", options =>
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

        services.AddAuthorization();

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapCarter();

        return app;
    }
}