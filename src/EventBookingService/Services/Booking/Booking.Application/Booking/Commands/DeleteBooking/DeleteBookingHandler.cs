using Booking.Application.Exceptions;

namespace Booking.Application.Booking.Commands.DeleteBooking;

public class DeleteBookingCommandHandler
    (IApplicationDbContext context)
    : ICommandHandler<DeleteBookingCommand, DeleteBookingResult> 
{
    public async Task<DeleteBookingResult> Handle(DeleteBookingCommand command, CancellationToken cancellationToken)
    {
        var bookingId = BookingId.Of(command.BookingId);
        var booking = await context.Bookings.FindAsync([bookingId], cancellationToken);

        if (booking is null)
            throw new BookingNotFoundException(command.BookingId);
        
        context.Bookings.Remove(booking);
        await context.SaveChangesAsync(cancellationToken);
        
        return new DeleteBookingResult(true);
    }
}