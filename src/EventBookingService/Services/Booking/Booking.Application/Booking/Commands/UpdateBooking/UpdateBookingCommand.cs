namespace Booking.Application.Booking.Commands.UpdateBooking;

public record UpdateBookingCommand(BookingDto Booking) : ICommand<UpdateBookingResult>;

public record UpdateBookingResult(bool IsSuccess);