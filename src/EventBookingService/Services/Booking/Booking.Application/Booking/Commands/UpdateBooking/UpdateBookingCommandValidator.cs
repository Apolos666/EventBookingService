namespace Booking.Application.Booking.Commands.UpdateBooking;

public class UpdateBookingCommandValidator : AbstractValidator<UpdateBookingCommand>
{
    public UpdateBookingCommandValidator()
    {
        RuleFor(x => x.Booking).NotNull().WithMessage("Booking cannot be null");
        RuleFor(x => x.Booking.UserId).NotEmpty().WithMessage("UserId cannot be empty");
        RuleFor(x => x.Booking.BookingItems).NotEmpty().WithMessage("BookingItems cannot be empty");
    }
}