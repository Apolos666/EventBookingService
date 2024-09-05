using EventBooking.Basket.Data;

namespace EventBooking.Basket.Features.StoreBasket;

public record StoreBasketCommand(EventCartDto Cart) : ICommand<StoreBasketResult>;

public record StoreBasketResult(Guid Id);

public class StoreBasketCommandHandler
    (IBasketRepository repository)
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        var result = await repository.StoreBasketAsync(command.Cart, cancellationToken);

        return new StoreBasketResult(result);
    }
}


