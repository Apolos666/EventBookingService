namespace EventBooking.Event.Extensions;

public static class AuthorizationExtension
{
    public static AuthorizationBuilder AddCustomAuthorizationPolicies(this AuthorizationBuilder services)
    {
        services.AddPolicy(nameof(EventBookingPolicy.UserOnly), policy =>
        {
            policy.RequireRealmRoles("user");
        });

        return services;
    }
}

