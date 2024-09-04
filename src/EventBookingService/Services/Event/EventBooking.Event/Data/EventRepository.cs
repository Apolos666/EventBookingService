namespace EventBooking.Event.Data;

public class EventRepository
    (IDocumentSession session)
    : IEventRepository
{
    public async Task<IEnumerable<Models.Event>> GetEventsAsync(int? pageNumber = 1, int? pageSize = 10, CancellationToken cancellationToken = default)
    {
        var events = await session.Query<Models.Event>()
            .ToPagedListAsync(pageNumber ?? 1, pageSize ?? 10, cancellationToken);

        return events;
    }

    public async Task<Models.Event> GetEventById(Guid eventId)
    {
        var @event = await session.LoadAsync<Models.Event>(eventId);

        if (@event == null)
            throw new EventNotFoundException(eventId);

        return @event;
    }

    public async Task<Guid> StoreEventAsync(EventDto eventDto, CancellationToken cancellationToken = default)
    {
        var @event = eventDto.ToEvent();

        session.Store(@event);
        await session.SaveChangesAsync(cancellationToken);

        return @event.Id;
    }

    public async Task<bool> DeleteEventAsync(Guid eventId, CancellationToken cancellationToken = default)
    {
        var @event = await session.LoadAsync<Models.Event>(eventId, cancellationToken);

        if (@event == null)
            throw new EventNotFoundException(eventId);

        session.Delete(@event);
        await session.SaveChangesAsync(cancellationToken);
        return true;
    }
}