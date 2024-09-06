namespace EventBooking.Basket.Extensions;

public static class BasketExtension
{
    public static EventCart ToEventCart(this EventCartDto eventCartDto)
    {
        var eventCart = new EventCart
        {
            CreatedAt = eventCartDto.CreatedAt,
            UpdatedAt = eventCartDto.UpdatedAt,
            Items = eventCartDto.Items.Select(ToEventCartItem).ToList()
        };
        
        return eventCart;
    }

    private static EventCartItem ToEventCartItem(this EventCartItemDto eventCartItemDto)
    {
        var eventCartItem = new EventCartItem
        {
            Id = Guid.NewGuid(),
            EventId = eventCartItemDto.EventId,
            EventLocationId = eventCartItemDto.EventLocationId,
            EventName = eventCartItemDto.EventName,
            Quantity = eventCartItemDto.Quantity,
            Price = eventCartItemDto.Price
        };

        return eventCartItem;
    } 
}