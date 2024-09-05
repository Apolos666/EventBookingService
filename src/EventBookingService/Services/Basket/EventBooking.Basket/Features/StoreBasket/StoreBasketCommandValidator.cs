namespace EventBooking.Basket.Features.StoreBasket;

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart)
            .NotNull()
            .WithMessage("Cart cannot be null");
        RuleFor(x => x.Cart.UserId)
            .NotEmpty()
            .WithMessage("UserId cannot be empty");
        RuleFor(x => x.Cart.Items)
            .NotNull()
            .WithMessage("Items cannot be null");
        RuleFor(x => x.Cart.Items)
            .Must(items => items.Count > 0)
            .WithMessage("Items must have at least one item");
        RuleFor(x => x.Cart.Items)
            .Must(items => items.All(x => x.Quantity > 0))
            .WithMessage("Quantity must be greater than 0");
        RuleFor(x => x.Cart.Items)
            .Must(items => items.All(x => x.Price > 0))
            .WithMessage("Price must be greater than 0");
    }
}


