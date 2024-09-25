namespace EventBooking.Event.Extensions;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication("webapp")
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

        return services;
    }
}