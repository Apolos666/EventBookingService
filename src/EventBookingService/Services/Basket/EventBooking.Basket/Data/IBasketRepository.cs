namespace EventBooking.Basket.Data;

public interface IBasketRepository
{
    Task<EventCart> GetBasketAsync(CancellationToken cancellationToken);
    Task<Guid> StoreBasketAsync(EventCartDto cartDto, CancellationToken cancellationToken);
    Task<bool> DeleteBasketAsync(CancellationToken cancellationToken);
}