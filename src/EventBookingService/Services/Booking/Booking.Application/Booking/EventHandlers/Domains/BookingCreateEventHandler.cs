using BookingCreatedEventObject = BuildingBlocks.Messaging.Events.Bookings.BookingCreatedEvent;
    
namespace Booking.Application.Booking.EventHandlers.Domains;

public class BookingCreateEventHandler
    (IPublishEndpoint publishEndpoint)
    : INotificationHandler<BookingCreatedEvent>
{
    // Todo: Add Send Email Notification (In Email Service or Something)
    public async Task Handle(BookingCreatedEvent message, CancellationToken cancellationToken)
    {
        var bookingCreatedEvent = new BookingCreatedEventObject
        {
            EventId = message.Booking.BookingItems.Select(x => x.EventId.Value).ToList(),
            UserId = message.Booking.UserId.Value
        };
        
        await publishEndpoint.Publish(bookingCreatedEvent, cancellationToken);
    }
}