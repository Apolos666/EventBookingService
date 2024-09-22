namespace BuildingBlocks.Messaging.Events.Payments;

public record SuccessCheckoutEvent : IntegrationEvent
{
    public Guid UserId { get; set; } = default!;

    public IEnumerable<CheckoutItemEvent> CheckoutEventItems { get; set; }
}

public record CheckoutItemEvent
{
    public Guid EventId { get; set; }
    public DateTime StartDateTime { get; set; }
    public Guid EventLocationId { get; set; }
    public string EventLocationName { get; set; }
    public string EventName { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}