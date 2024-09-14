namespace EventBooking.Basket.Data;

public class BasketRepository
    (IDocumentSession session, IUserIdentityAccessor userIdentityAccessor)
    : IBasketRepository       
{
    public async Task<EventCart> GetBasketAsync(CancellationToken cancellationToken)
    {
        var eventCart = await session.LoadAsync<EventCart>(Guid.Parse(userIdentityAccessor.UserId), cancellationToken);

        if (eventCart is null)
            throw new BasketNotFoundException(Guid.Parse(userIdentityAccessor.UserId));
        
        return eventCart;
    }

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
        eventCart.UserId = Guid.Parse(userIdentityAccessor.UserId);
        
        session.Store(eventCart);
        await session.SaveChangesAsync(cancellationToken);
        
        return eventCart.UserId;
    }

    public async Task<bool> DeleteBasketAsync(CancellationToken cancellationToken)
    {
        session.Delete<EventCart>(Guid.Parse(userIdentityAccessor.UserId));
        await session.SaveChangesAsync(cancellationToken);
        return true;
    }
}