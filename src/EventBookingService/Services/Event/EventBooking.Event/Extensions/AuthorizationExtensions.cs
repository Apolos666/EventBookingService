namespace EventBooking.Event.Extensions;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddCustomAuthorization(this IServiceCollection services)
    {
        services
            .AddAuthorization()
            .AddKeycloakAuthorization()
            .AddAuthorizationBuilder()
            .AddCustomAuthorizationPolicies();

        return services;
    }
}