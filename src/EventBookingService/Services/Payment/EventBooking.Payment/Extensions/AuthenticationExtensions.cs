namespace EventBooking.Payment.Extensions;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
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

        services.AddAuthorization();

        return services;
    }
}