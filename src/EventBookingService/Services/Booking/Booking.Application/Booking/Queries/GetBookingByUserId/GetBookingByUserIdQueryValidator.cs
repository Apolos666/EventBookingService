namespace Booking.Application.Booking.Queries.GetBookingByUserId;

public class GetBookingByUserIdQueryValidator : AbstractValidator<GetBookingByUserIdQuery>
{
    public GetBookingByUserIdQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
    }
}