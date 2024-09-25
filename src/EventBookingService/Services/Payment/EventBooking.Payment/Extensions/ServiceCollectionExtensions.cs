namespace EventBooking.Payment.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCarter();
        services.AddDistributedMemoryCache();
        services.AddHttpContextAccessor();
        services.AddScoped<IUserIdentityAccessor, HttpUserIdentityAccessor>();
        services.AddScoped<IPaymentService<StripeCheckoutRequest, StripeCheckoutResponse>>(serviceProvider =>
        {
            var publisher = serviceProvider.GetRequiredService<IPublishEndpoint>();
            var logger = serviceProvider.GetRequiredService<ILogger<StripePaymentService>>();
            return new StripePaymentService(
                configuration["Stripe:Secret"],
                publisher,
                logger);
        });

        return services;
    }

    public static IServiceCollection AddMessageBrokerServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());

        return services;
    }
}