namespace Booking.Application.Booking.Commands.UpdateBooking;

public class UpdateBookingHandler
    (IApplicationDbContext context)
    : ICommandHandler<UpdateBookingCommand, UpdateBookingResult>
{
    public async Task<UpdateBookingResult> Handle(UpdateBookingCommand command, CancellationToken cancellationToken)
    {
        var bookingId = BookingId.Of(command.Booking.Id);
        var booking = await context.Bookings.FindAsync([bookingId], cancellationToken);
        
        if (booking is null)
            throw new BookingNotFoundException(command.Booking.Id);

        UpdateBookingWithNewValues(booking, command.Booking);

        context.Bookings.Update(booking);
        await context.SaveChangesAsync(cancellationToken);
        
        return new UpdateBookingResult(true);
    }
    
    // Todo: Implement this method
    private void UpdateBookingWithNewValues(BookingModel.Booking booking, BookingDto bookingDto)
    {
        
    }
}