namespace EventBooking.Basket.Data;

public class CachedBasketRepository
    (IBasketRepository repository, IDistributedCache cache)
    : IBasketRepository
{
    private static readonly TimeSpan DefaultExpiration = TimeSpan.FromMinutes(30);
    
    private async Task SetCacheAsync(string key, string value, CancellationToken cancellationToken)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = DefaultExpiration
        };
        await cache.SetStringAsync(key, value, options, cancellationToken);
    }
    
    public async Task<EventCart> GetBasketAsync(Guid userId, CancellationToken cancellationToken)
    {
        var cacheKey = $"basket_{userId}";
        var cachedBasket = await cache.GetStringAsync(cacheKey, cancellationToken);
        
        if (!string.IsNullOrEmpty(cachedBasket))
            return JsonSerializer.Deserialize<EventCart>(cachedBasket)!;
        
        var basket = await repository.GetBasketAsync(userId, cancellationToken);
        await SetCacheAsync(cacheKey, JsonSerializer.Serialize(basket), cancellationToken);
        return basket;
    }

    public async Task<Guid> StoreBasketAsync(EventCartDto cartDto, CancellationToken cancellationToken)
    {
        var result = await repository.StoreBasketAsync(cartDto, cancellationToken);
        
        var cacheKey = $"basket_{cartDto.UserId}";
        
        await SetCacheAsync(cacheKey, JsonSerializer.Serialize(cartDto.ToEventCart()), cancellationToken);
        
        return result;
    }

    public async Task<bool> DeleteBasketAsync(Guid userId, CancellationToken cancellationToken)
    {
        var result = await repository.DeleteBasketAsync(userId, cancellationToken);
        
        var cacheKey = $"basket_{userId}";
        
        await cache.RemoveAsync(cacheKey, cancellationToken);
        
        return result;
    }
}