namespace EventBooking.Basket.Data;

public class BasketRepository
    (IDocumentSession session)
    : IBasketRepository       
{
    public async Task<EventCart> GetBasketAsync(Guid userId, CancellationToken cancellationToken)
    {
        var eventCart = await session.LoadAsync<EventCart>(userId, cancellationToken);

        if (eventCart is null)
            throw new BasketNotFoundException(userId);
        
        return eventCart;
    }

    public async Task<Guid> StoreBasketAsync(EventCartDto cartDto, CancellationToken cancellationToken)
    {
        var eventCart = cartDto.ToEventCart();
        
        session.Store(eventCart);
        await session.SaveChangesAsync(cancellationToken);
        
        return eventCart.UserId;
    }

    public async Task<bool> DeleteBasketAsync(Guid userId, CancellationToken cancellationToken)
    {
        session.Delete<EventCart>(userId);
        await session.SaveChangesAsync(cancellationToken);
        return true;
    }
}