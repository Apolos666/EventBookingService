namespace EventBooking.Basket.Dtos;

public record EventCartDto
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<EventCartItemDto> Items { get; set; } = [];
}