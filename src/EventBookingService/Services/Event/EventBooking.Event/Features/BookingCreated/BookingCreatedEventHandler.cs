namespace EventBooking.Event.Features.BookingCreated;

public class BookingCreatedEventHandler
    (IDocumentSession session)
    : IConsumer<BookingCreatedEvent>
{
    public async Task Consume(ConsumeContext<BookingCreatedEvent> context)
    {
        foreach (var eventId in context.Message.EventId)
        {
            var @event = await session.LoadAsync<Models.Event>(eventId);

            if (@event is null)
                continue;
                //throw new EventNotFoundException(eventId);
            
            @event.UserRegistedId.Add(context.Message.UserId);
            session.Store(@event);
        }
        
        await session.SaveChangesAsync();
    }
}