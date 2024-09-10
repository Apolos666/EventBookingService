namespace Booking.Application.Booking.EventHandlers.Integrations;

public class BasketCheckoutEventHandler
    (ISender sender, ILogger<BasketCheckoutEventHandler> logger)
    : IConsumer<BasketCheckoutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);
        
        var command = MapToCreateBookingCommand(context.Message);
        await sender.Send(command);
    }
    
    private CreateBookingCommand MapToCreateBookingCommand(BasketCheckoutEvent basketCheckoutEvent)
    {
        var bookingId = Guid.NewGuid();

        var bookingDto = new BookingDto(
            bookingId,
            basketCheckoutEvent.UserId,
            basketCheckoutEvent.BasketCheckoutEventItems.Select(bi => new BookingItemDto(
                bookingId,
                bi.EventId,
                bi.StartDateTime,
                bi.EventLocationId,
                bi.EventName,
                bi.Quantity,
                bi.Price
            )).ToList());
        
        return new CreateBookingCommand(bookingDto);
    }
}