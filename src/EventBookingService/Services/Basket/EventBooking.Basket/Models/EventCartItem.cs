namespace EventBooking.Basket.Models;

public sealed class EventCartItem
{
    public Guid Id { get; set; }
    public Guid EventId { get; set; }
    public Guid EventLocationId { get; set; }
    public string EventName { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}