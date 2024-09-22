namespace EventBooking.Event.Data;

public class CachedEventRepository(IEventRepository eventRepository, IDistributedCache cache, IUserIdentityAccessor userIdentityAccessor)
    : IEventRepository
{
    private const string VERSION_KEY = "events_version";
    private static readonly TimeSpan DefaultExpiration = TimeSpan.FromMinutes(30);

    // Retrieves or creates a version key for cache invalidation
    private async Task<string> GetOrCreateVersionAsync(CancellationToken cancellationToken = default)
    {
        var version = await cache.GetStringAsync(VERSION_KEY, cancellationToken);
        if (!string.IsNullOrEmpty(version)) return version;
        version = Guid.NewGuid().ToString();
        await SetCacheAsync(VERSION_KEY, version, cancellationToken);
        return version;
    }

    // Sets a new version key to invalidate the cache
    private async Task InvalidateCacheAsync(CancellationToken cancellationToken = default)
    {
        await SetCacheAsync(VERSION_KEY, Guid.NewGuid().ToString(), cancellationToken);
    }

    // Sets a value in the cache with a specified expiration time
    private Task SetCacheAsync(string key, string value, CancellationToken cancellationToken = default)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = DefaultExpiration
        };
        return cache.SetStringAsync(key, value, options, cancellationToken);
    }

    // Retrieves events from the cache or the repository if not cached
    public async Task<IEnumerable<Models.Event>> GetEventsAsync(int? pageNumber, int? pageSize,
        CancellationToken cancellationToken = default)
    {
        var version = await GetOrCreateVersionAsync(cancellationToken);
        var cachedKey = $"events_{version}_{pageNumber}_{pageSize}";

        var cachedEvents = await cache.GetStringAsync(cachedKey, cancellationToken);
        if (!string.IsNullOrEmpty(cachedEvents))
            return JsonSerializer.Deserialize<IEnumerable<Models.Event>>(cachedEvents)!;

        var events = await eventRepository.GetEventsAsync(pageNumber, pageSize, cancellationToken);
        await SetCacheAsync(cachedKey, JsonSerializer.Serialize(events), cancellationToken);
        return events;
    }

    // Retrieves a specific event by ID from the cache or the repository if not cached
    public async Task<Models.Event> GetEventById(Guid eventId, CancellationToken cancellationToken)
    {
        var cachedKey = $"event_{eventId}";
        var cachedEvent = await cache.GetStringAsync(cachedKey, cancellationToken);
        if (!string.IsNullOrEmpty(cachedEvent))
            return JsonSerializer.Deserialize<Models.Event>(cachedEvent)!;

        var @event = await eventRepository.GetEventById(eventId, cancellationToken);
        await SetCacheAsync(cachedKey, JsonSerializer.Serialize(@event), cancellationToken);
        return @event;
    }

    // Stores an event and invalidates the cache
    public async Task<Guid> StoreEventAsync(EventDto eventDto, string imageUrl, CancellationToken cancellationToken = default)
    {
        var result = await eventRepository.StoreEventAsync(eventDto, imageUrl, cancellationToken);
        var cachedKey = $"event_{result}";
        
        var @event = eventDto.ToEvent();
        @event.Id = result;
        @event.EventImageUrl = imageUrl;
        @event.HostId = Guid.Parse(userIdentityAccessor.UserId);
        
        await SetCacheAsync(cachedKey, JsonSerializer.Serialize(@event), cancellationToken);

        await InvalidateCacheAsync(cancellationToken);

        return result;
    }

    // Deletes an event and invalidates the cache
    public async Task<bool> DeleteEventAsync(Guid eventId, CancellationToken cancellationToken = default)
    {
        var cachedKey = $"event_{eventId}";
        await cache.RemoveAsync(cachedKey, cancellationToken);

        await InvalidateCacheAsync(cancellationToken);

        return await eventRepository.DeleteEventAsync(eventId, cancellationToken);
    }
}