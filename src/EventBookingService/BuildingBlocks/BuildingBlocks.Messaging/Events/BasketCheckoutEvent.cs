namespace BuildingBlocks.Messaging.Events;

public record BasketCheckoutEvent : IntegrationEvent
{
    public Guid UserId { get; set; } = default!;

    public IEnumerable<BasketCheckoutEventItem> BasketCheckoutEventItems { get; set; } 
}

public record BasketCheckoutEventItem
{
    public Guid EventId { get; set; }
    public Guid EventLocationId { get; set; }
    public string EventName { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}