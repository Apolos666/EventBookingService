namespace Booking.Application.Booking.Commands.CreateBooking;

public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
{
    public CreateBookingCommandValidator()
    {
        RuleFor(x => x.Booking).NotNull().WithMessage("Booking cannot be null");
        RuleFor(x => x.Booking.UserId).NotEmpty().WithMessage("UserId cannot be empty");
        RuleFor(x => x.Booking.BookingItems).NotEmpty().WithMessage("BookingItems cannot be empty");
    }
}