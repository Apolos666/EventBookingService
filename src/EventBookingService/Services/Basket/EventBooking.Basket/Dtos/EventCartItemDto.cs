namespace EventBooking.Basket.Dtos;

public record EventCartItemDto
{
    public Guid EventId { get; set; }
    public DateTime StartDateTime { get; set; }
    public Guid EventLocationId { get; set; }
    public string EventName { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}