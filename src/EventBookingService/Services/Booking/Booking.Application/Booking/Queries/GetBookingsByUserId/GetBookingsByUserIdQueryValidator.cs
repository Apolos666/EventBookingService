namespace Booking.Application.Booking.Queries.GetBookingsByUserId;

public class GetBookingsByUserIdQueryValidator : AbstractValidator<GetBookingsByUserIdQuery>
{
    public GetBookingsByUserIdQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
    }
}