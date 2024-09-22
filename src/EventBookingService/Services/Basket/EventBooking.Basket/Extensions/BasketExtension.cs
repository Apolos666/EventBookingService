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

    public static EventCartItem ToEventCartItem(this EventCartItemDto eventCartItemDto)
    {
        var eventCartItem = new EventCartItem
        {
            Id = Guid.NewGuid(),
            EventId = eventCartItemDto.EventId,
            StartDateTime = eventCartItemDto.StartDateTime,
            EventLocationId = eventCartItemDto.EventLocationId,
            EventLocationName = eventCartItemDto.EventLocationName,
            EventName = eventCartItemDto.EventName,
            Quantity = eventCartItemDto.Quantity,
            Price = eventCartItemDto.Price
        };

        return eventCartItem;
    } 
}