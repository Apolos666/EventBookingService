namespace EventBooking.Event.Features.GetEventsUser;

public record GetEventsUserQuery() : IQuery<GetEventsUserResult>;

public record GetEventsUserResult(IEnumerable<Models.Event> Events);

public class GetEventsUserQueryHandler
    (IEventRepository repository) 
    : IQueryHandler<GetEventsUserQuery, GetEventsUserResult>
{
    public async Task<GetEventsUserResult> Handle(GetEventsUserQuery query, CancellationToken cancellationToken)
    {
        var events = await repository.GetEventsUserAsync(cancellationToken);
        
        return new GetEventsUserResult(events);
    }
}