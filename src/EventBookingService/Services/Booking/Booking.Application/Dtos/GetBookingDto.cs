namespace Booking.Application.Dtos;

public class GetBookingDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string BookingStatus { get; set; }
    public int TotalQuantity { get; set; }
    public decimal TotalPrice { get; set; }
    public List<GetBookingItemDto> BookingItems { get; set; } = [];
}