namespace EventBooking.Event.Extensions;

public static class GrpcExtensions
{
    public static IServiceCollection AddGrpcClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpcClient<ImageStorage.ImageStorageClient>(options =>
            {
                options.Address = new Uri(configuration["GrpcSettings:StorageUrl"]);
            })
            .AddCallCredentials(async (context, metadata, serviceProvider) =>
            {
                var provider = serviceProvider.GetRequiredService<IClientCredentialsTokenManagementService>();
                var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                
                var response = await provider.GetAccessTokenAsync("storage.client");
            
                metadata.Add("Authorization", $"Bearer {response.AccessToken}");
            })
            .ConfigurePrimaryHttpMessageHandler(_ =>
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };
                
                return handler;
            });

        return services;
    }
}