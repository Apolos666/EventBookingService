namespace EventBooking.Basket.Extensions;

public static class GrpcExtensions
{
    public static IServiceCollection AddGrpcServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpc();
        services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
        {
            options.Address = new Uri(configuration["GrpcSettings:DiscountUrl"]!);
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