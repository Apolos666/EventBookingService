namespace Booking.Application.Booking.Commands.DeleteBooking;

public class DeleteBookingCommandValidator : AbstractValidator<DeleteBookingCommand>
{
    public DeleteBookingCommandValidator()
    {
        RuleFor(x => x.BookingId).NotEmpty().WithMessage("BookingId cannot be empty");
    }
}