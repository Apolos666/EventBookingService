namespace EventBooking.Basket.Extensions;

public static class AuthenticationExtensions
    {
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication("webapp")
                .AddJwtBearer("webapp", ConfigureWebAppJwtBearer(configuration))
                .AddJwtBearer("basket_service", ConfigureBasketServiceJwtBearer(configuration));

            return services;
        }

        private static Action<JwtBearerOptions> ConfigureWebAppJwtBearer(IConfiguration configuration)
        {
            return options =>
            {
                options.Authority = configuration["Keycloak:Authority"];
                options.MetadataAddress = configuration["Keycloak:MetadataAddress"];
                options.RequireHttpsMetadata = configuration.GetValue<bool>("Keycloak:RequireHttpsMetadata");
                options.Audience = configuration["Keycloak:Audience:webapp"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.Name,
                    RoleClaimType = ClaimTypes.Role,
                    ValidateIssuer = false,
                    ValidIssuers = configuration.GetValue<IEnumerable<string>>("Keycloak:ValidIssuers"),
                    ValidateAudience = true,
                    ValidAudiences = configuration.GetValue<IEnumerable<string>>("Keycloak:ValidAudiences:webapp")
                };
            };
        }

        private static Action<JwtBearerOptions> ConfigureBasketServiceJwtBearer(IConfiguration configuration)
        {
            return options =>
            {
                options.Authority = configuration["Keycloak:Authority"];
                options.MetadataAddress = configuration["Keycloak:MetadataAddress"];
                options.RequireHttpsMetadata = configuration.GetValue<bool>("Keycloak:RequireHttpsMetadata");
                options.Audience = configuration["Keycloak:Audience:basket_service"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.Name,
                    RoleClaimType = ClaimTypes.Role,
                    ValidateIssuer = true,
                    ValidIssuers = configuration.GetValue<IEnumerable<string>>("Keycloak:ValidIssuers"),
                    ValidateAudience = true,
                    ValidAudiences = configuration.GetValue<IEnumerable<string>>("Keycloak:ValidAudiences:basket_service")
                };
            };
        }
    }