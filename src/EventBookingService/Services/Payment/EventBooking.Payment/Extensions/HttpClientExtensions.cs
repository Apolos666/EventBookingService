namespace EventBooking.Payment.Extensions;

public static class HttpClientExtensions
{
    public static IServiceCollection AddCustomHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddClientCredentialsTokenManagement()
            .AddClient("basket.client", client =>
            {
                client.TokenEndpoint = configuration["Keycloak:TokenEndpoint"];
                client.ClientId = configuration["Keycloak:ClientId"];
                client.ClientSecret = configuration["Keycloak:ClientSecret"];
            });

        return services;
    }
}