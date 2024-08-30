using FluentValidation;

namespace EventBooking.Event.Features.StoreEvent;

public class StoreEventCommandValidator : AbstractValidator<StoreEventCommand>
{
    public StoreEventCommandValidator()
    {
        RuleFor(x => x.Event).NotNull().WithMessage("Cart can not be null");
        RuleFor(x => x.Event.Name).NotEmpty().WithMessage("Name can not be empty");
        RuleFor(x => x.Event.StartDateTime)
            .NotEmpty().WithMessage("StartDateTime can not be empty")
            .LessThan(x => x.Event.EndDateTime).WithMessage("StartDateTime can not be greater than EndDateTime");
        RuleFor(x => x.Event.EndDateTime)
            .NotEmpty().WithMessage("EndDateTime can not be empty")
            .GreaterThan(x => x.Event.StartDateTime).WithMessage("EndDateTime can not be less than StartDateTime");
        RuleFor(x => x.Event.EventLocationDtos).NotNull().WithMessage("EventLocationDtos can not be null");
    }
}