namespace Booking.Application.Booking.EventHandlers.Domains;

// TODO: Implement the BookingCreateEventHandler
public class BookingCreateEventHandler
    ()
    : INotificationHandler<BookingCreatedEvent>
{
    public Task Handle(BookingCreatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}