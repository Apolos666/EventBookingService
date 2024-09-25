namespace EventBooking.Event.Extensions;

public static class HttpClientExtensions
{
    public static IServiceCollection AddCustomHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddClientCredentialsTokenManagement()
            .AddClient("storage.client", client =>
            {
                client.TokenEndpoint = configuration["Keycloak:TokenEndpoint"];
                client.ClientId = configuration["Keycloak:ClientIdStorage"];
                client.ClientSecret = configuration["Keycloak:ClientSecretStorage"];
            });

        return services;
    }
}