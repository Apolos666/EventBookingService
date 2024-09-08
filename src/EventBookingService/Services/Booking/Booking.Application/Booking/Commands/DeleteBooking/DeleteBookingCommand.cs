namespace Booking.Application.Booking.Commands.DeleteBooking;

public record DeleteBookingCommand(Guid BookingId) : ICommand<DeleteBookingResult>;

public record DeleteBookingResult(bool IsSuccess);