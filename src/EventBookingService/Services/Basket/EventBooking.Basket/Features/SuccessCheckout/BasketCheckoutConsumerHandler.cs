namespace EventBooking.Basket.Features.SuccessCheckout;

public class BasketCheckoutConsumerHandler
    (ILogger<BasketCheckoutConsumerHandler> logger, IBasketRepository repository)
    : IConsumer<SuccessCheckoutEvent>
{
    public async Task Consume(ConsumeContext<SuccessCheckoutEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);
        
        await repository.DeleteBasketAsync(context.Message.UserId, cancellationToken: context.CancellationToken);
        
        logger.LogInformation("Basket deleted for user {UserId}", context.Message.UserId);
    }
}