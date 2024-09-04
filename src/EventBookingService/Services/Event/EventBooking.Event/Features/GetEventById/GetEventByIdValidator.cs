namespace EventBooking.Event.Features.GetEventById;

public class GetEventByIdValidator : AbstractValidator<GetEventByIdQuery>
{
    public GetEventByIdValidator()
    {
        RuleFor(x => x.EventId).NotNull().WithMessage("Name is required");
    }
}