namespace Booking.Application.Booking.EventHandlers.Integrations;

public class SuccessCheckoutEventHandler
    (ILogger<SuccessCheckoutEventHandler> logger, ISender sender)
    : IConsumer<SuccessCheckoutEvent>
{
    public async Task Consume(ConsumeContext<SuccessCheckoutEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);
        
        var command = MapToCreateBookingCommand(context.Message);
        await sender.Send(command);
    }
    
    private CreateBookingCommand MapToCreateBookingCommand(SuccessCheckoutEvent successCheckoutEvent)
    {
        var bookingId = Guid.NewGuid();

        var bookingDto = new BookingDto(
            bookingId,
            successCheckoutEvent.UserId,
            successCheckoutEvent.CheckoutEventItems.Select(bi => new BookingItemDto(
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