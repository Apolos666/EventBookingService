namespace EventBooking.Basket.Features.StoreBasket;

public record StoreBasketCommand(EventCartDto Cart) : ICommand<StoreBasketResult>;

public record StoreBasketResult(EventCart Cart);

public class StoreBasketCommandHandler
    (IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountProtoClient)
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        foreach (var item in command.Cart.Items)
        {
            await DeductDiscountAsync(item, cancellationToken);
        }
        
        var result = await repository.StoreBasketAsync(command.Cart, cancellationToken);

        return new StoreBasketResult(result);
    }
    
    private async Task DeductDiscountAsync(EventCartItemDto item, CancellationToken cancellationToken)
    {
        var discount = await discountProtoClient
            .GetDiscountAsync(new GetDiscountRequest { EventName = item.EventName }, cancellationToken: cancellationToken);

        item.Price -= item.Price * discount.DiscountPercentage / 100;
    }
}


