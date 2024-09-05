namespace EventBooking.Basket.Dtos;

public record EventCartDto
{
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<EventCartItemDto> Items { get; set; } = [];
}

public record EventCartItemDto
{
    public Guid EventId { get; set; }
    public Guid EventLocationId { get; set; }
    public string EventName { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}