namespace Booking.Application.Dtos;

public class GetBookingItemDto
{
    public Guid BookingId { get; set; }
    public Guid EventId { get; set; }
    public DateTime StartDateTime { get; set; }
    public Guid EventLocationId { get; set; }
    public string EventLocationName { get; set; }
    public string EventName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }
    public string Code { get; set; }
}