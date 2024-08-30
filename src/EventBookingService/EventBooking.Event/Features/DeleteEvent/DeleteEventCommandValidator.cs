namespace EventBooking.Event.Features.DeleteEvent;

public class DeleteEventCommandValidator : AbstractValidator<DeleteEventCommand>
{
    public DeleteEventCommandValidator()
    {
        RuleFor(x => x.EventId).NotEmpty().WithMessage("Event id is required");
    }
}