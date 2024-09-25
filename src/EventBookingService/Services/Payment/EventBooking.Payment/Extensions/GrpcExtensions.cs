namespace EventBooking.Payment.Extensions;

public static class GrpcExtensions
{
    public static IServiceCollection AddGrpcClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpcClient<BasketProtoService.BasketProtoServiceClient>(options =>
        {
            options.Address = new Uri(configuration["GrpcSettings:BasketUrl"]);
        }).AddCallCredentials(async (context, metadata, serviceProvider) =>
        {
            var provider = serviceProvider.GetRequiredService<IClientCredentialsTokenManagementService>();
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            var response = await provider.GetAccessTokenAsync("basket.client");

            logger.LogInformation("Access token: {accessToken}", response.AccessToken);
                
            metadata.Add("Authorization", $"Bearer {response.AccessToken}");
        }).ConfigurePrimaryHttpMessageHandler(_ =>
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