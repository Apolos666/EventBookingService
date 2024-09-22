namespace EventBooking.Event.Data;

public interface IEventRepository
{
    Task<IEnumerable<Models.Event>> GetEventsAsync(int? pageNumber = 1, int? pageSize = 10, CancellationToken cancellationToken = default);
    Task<Models.Event> GetEventById(Guid eventId, CancellationToken cancellationToken = default);
    Task<Guid> StoreEventAsync(EventDto eventDto, string imageUrl, CancellationToken cancellationToken = default);
    Task<bool> DeleteEventAsync(Guid eventId, CancellationToken cancellationToken = default);
}