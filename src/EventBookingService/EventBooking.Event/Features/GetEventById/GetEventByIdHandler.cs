namespace EventBooking.Event.Features.GetEventById;

public record GetEventByIdQuery(Guid EventId) : IQuery<GetEventByIdResult>;

public record GetEventByIdResult(Models.Event Event);

public class GetEventByIdHandler
    (IEventRepository repository)
    : IQueryHandler<GetEventByIdQuery, GetEventByIdResult>
{
    public async Task<GetEventByIdResult> Handle(GetEventByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await repository.GetEventById(query.EventId);
        return new GetEventByIdResult(result);
    }
}