namespace EventBooking.Event.Data;

public class CachedEventRepository
    (IEventRepository eventRepository, IDistributedCache cache)
    : IEventRepository
{
    private const string VERSION_KEY = "events_version";
    private static readonly TimeSpan DefaultExpiration = TimeSpan.FromMinutes(30);

    private async Task<string> GetOrCreateVersionAsync(CancellationToken cancellationToken = default)
    {
        var version = await cache.GetStringAsync(VERSION_KEY, cancellationToken);
        if (!string.IsNullOrEmpty(version)) return version;
        version = Guid.NewGuid().ToString();
        await SetCacheAsync(VERSION_KEY, version, cancellationToken);
        return version;
    }

    /// <summary>
    /// Set a new version key to invalidate the cache
    /// </summary>
    private async Task InvalidateCacheAsync(CancellationToken cancellationToken = default)
    {
        await SetCacheAsync(VERSION_KEY, Guid.NewGuid().ToString(), cancellationToken);
    }

    private Task SetCacheAsync(string key, string value, CancellationToken cancellationToken = default)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = DefaultExpiration
        };
        return cache.SetStringAsync(key, value, options, cancellationToken);
    }

    public async Task<IEnumerable<Models.Event>> GetEventsAsync(int? pageNumber, int? pageSize, CancellationToken cancellationToken = default)
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

    public async Task<Models.Event> GetEventById(Guid eventId)
    {
        var cachedKey = $"event_{eventId}";
        var cachedEvent = await cache.GetStringAsync(cachedKey);
        if (!string.IsNullOrEmpty(cachedEvent))
            return JsonSerializer.Deserialize<Models.Event>(cachedEvent)!;

        var @event = await eventRepository.GetEventById(eventId);
        await SetCacheAsync(cachedKey, JsonSerializer.Serialize(@event));
        return @event;
    }

    public async Task<Guid> StoreEventAsync(EventDto eventDto, CancellationToken cancellationToken = default)
    {
        var result = await eventRepository.StoreEventAsync(eventDto, cancellationToken);
        var cachedKey = $"event_{result}";
        await SetCacheAsync(cachedKey, JsonSerializer.Serialize(eventDto.ToEvent()), cancellationToken);

        await InvalidateCacheAsync(cancellationToken);

        return result;
    }

    public async Task<bool> DeleteEventAsync(Guid eventId, CancellationToken cancellationToken = default)
    {
        var cachedKey = $"event_{eventId}";
        await cache.RemoveAsync(cachedKey, cancellationToken);

        await InvalidateCacheAsync(cancellationToken);

        return await eventRepository.DeleteEventAsync(eventId, cancellationToken);
    }
}