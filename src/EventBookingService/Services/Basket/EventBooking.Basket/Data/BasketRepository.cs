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

    public async Task<EventCart> StoreBasketAsync(EventCartDto cartDto, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(userIdentityAccessor.UserId);
        var existingCart = await session.LoadAsync<EventCart>(userId, cancellationToken);

        if (existingCart is null)
        {
            var newCart = cartDto.ToEventCart();
            newCart.UserId = userId;
            session.Store(newCart);
        }
        else
        {
            foreach (var newItem in cartDto.Items)
            {
                var exisitingItem = existingCart.Items.FirstOrDefault(x => x.EventLocationId == newItem.EventLocationId);
                if (exisitingItem is not null)
                {
                    exisitingItem.Quantity += newItem.Quantity;
                }
                else
                {
                    existingCart.Items.Add(newItem.ToEventCartItem());
                }
            }
            
            existingCart.UpdatedAt = DateTime.UtcNow;
            session.Update(existingCart);
        }
        
        await session.SaveChangesAsync(cancellationToken);
        return existingCart; 
    }

    public async Task<bool> DeleteBasketAsync(CancellationToken cancellationToken)
    {
        session.Delete<EventCart>(Guid.Parse(userIdentityAccessor.UserId));
        await session.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteBasketAsync(Guid userId, CancellationToken cancellationToken)
    {
        session.Delete<EventCart>(userId);
        await session.SaveChangesAsync(cancellationToken);
        return true;
    }
}