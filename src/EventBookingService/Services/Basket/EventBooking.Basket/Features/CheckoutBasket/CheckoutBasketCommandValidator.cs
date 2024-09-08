namespace EventBooking.Basket.Features.CheckoutBasket;

public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketCommandValidator()
    {
        // RuleFor(x => x.CheckoutBasketDto).NotNull().WithMessage("CheckoutBasketDto is required");
        // RuleFor(x => x.CheckoutBasketDto.BasketCheckoutEventItems).NotEmpty().WithMessage("BasketCheckoutEventItems is required");
    }
}