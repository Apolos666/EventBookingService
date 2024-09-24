namespace EventBooking.Event.Data;

public class EventRepository
    (IDocumentSession session, IUserIdentityAccessor userIdentityAccessor)
    : IEventRepository
{
    public async Task<IEnumerable<Models.Event>> GetEventsAsync(int? pageNumber = 1, int? pageSize = 10, CancellationToken cancellationToken = default)
    {
        var events = await session.Query<Models.Event>()
            .ToPagedListAsync(pageNumber ?? 1, pageSize ?? 10, cancellationToken);

        return events;
    }

    public async Task<IEnumerable<Models.Event>> GetEventsUserAsync(CancellationToken cancellationToken = default)
    {
        var events = await session.Query<Models.Event>()
            .Where(x => x.HostId == Guid.Parse(userIdentityAccessor.UserId))
            .ToListAsync(cancellationToken);
        
        if (events is null)
            throw new EventNotFoundException(Guid.Parse(userIdentityAccessor.UserId));
        
        return events;
    }

    public async Task<Models.Event> GetEventById(Guid eventId, CancellationToken cancellationToken = default)
    {
        var @event = await session.LoadAsync<Models.Event>(eventId, cancellationToken);

        if (@event == null)
            throw new EventNotFoundException(eventId);

        return @event;
    }

    public async Task<Models.Event> StoreEventAsync(EventDto eventDto, string imageUrl, CancellationToken cancellationToken = default)
    {
        var @event = eventDto.ToEvent();

        @event.HostId = Guid.Parse(userIdentityAccessor.UserId);
        @event.EventImageUrl = imageUrl;

        session.Store(@event);
        await session.SaveChangesAsync(cancellationToken);

        return @event;
    }

    public async Task<bool> DeleteEventAsync(Guid eventId, CancellationToken cancellationToken = default)
    {
        var @event = await session.LoadAsync<Models.Event>(eventId, cancellationToken);
        
        if (@event == null)
            throw new EventNotFoundException(eventId);
        
        if (@event.HostId != Guid.Parse(userIdentityAccessor.UserId))
            throw new UnauthorizedEventDeletionException(userIdentityAccessor.UserId);

        session.Delete(@event);
        await session.SaveChangesAsync(cancellationToken);
        return true;
    }
}