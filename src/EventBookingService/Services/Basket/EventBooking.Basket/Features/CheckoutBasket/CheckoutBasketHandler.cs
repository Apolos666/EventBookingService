namespace EventBooking.Basket.Features.CheckoutBasket;

public record CheckoutBasketCommand() : ICommand<CheckoutBasketResult>;

public record CheckoutBasketResult(bool IsSuccess);

public class CheckoutBasketCommandHandler
    (IBasketRepository repository, IPublishEndpoint publishEndpoint, IUserIdentityAccessor userIdentityAccessor)
    : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        var basket = await repository.GetBasketAsync(cancellationToken);
        
        if (basket is null)
            throw new BasketNotFoundException(Guid.Parse(userIdentityAccessor.UserId));

        var basketCheckoutEvent = new BasketCheckoutEvent
        {
            UserId = Guid.Parse(userIdentityAccessor.UserId),
            BasketCheckoutEventItems = basket.Items.Select(bi => new BasketCheckoutEventItem
            {
                EventId = bi.EventId,
                StartDateTime = bi.StartDateTime,
                EventLocationId = bi.EventLocationId,
                EventName = bi.EventName,
                Quantity = bi.Quantity,
                Price = bi.Price
            })
        };

        await publishEndpoint.Publish(basketCheckoutEvent, cancellationToken);

        await repository.DeleteBasketAsync(cancellationToken);
        
        return new CheckoutBasketResult(true);
    }
}