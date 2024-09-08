namespace Booking.Application.Booking.EventHandlers.Domains;

// TODO: Implement the BookingCreateEventHandler
public class BookingCreateEventHandler
    ()
    : INotificationHandler<BookingCreatedEvent>
{
    // Todo: Add Send Email Notification (In Email Service or Something)
    public Task Handle(BookingCreatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}