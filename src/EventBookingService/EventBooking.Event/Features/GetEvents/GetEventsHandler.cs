using EventBooking.Event.Data;

namespace EventBooking.Event.Features.GetEvents;

public record GetEventsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetEventsResult>;

public record GetEventsResult(IEnumerable<Models.Event> Events);

public class GetEventsHandler
    (IEventRepository repository) 
    : IQueryHandler<GetEventsQuery, GetEventsResult>
{
    public async Task<GetEventsResult> Handle(GetEventsQuery query, CancellationToken cancellationToken)
    {
        var events = await repository.GetEventsAsync(query.PageNumber, query.PageSize, cancellationToken);

        return new GetEventsResult(events);
    }
}