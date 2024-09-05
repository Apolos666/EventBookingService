namespace EventBooking.Event.Models;

public sealed class EventLocation
{
    public Guid Id { get; set; }
    public Location Location { get; set; } =  default!;
    public int MaxAttendees { get; set; }
    public int RegisteredAttendees { get; set; }
    public decimal Price { get; set; }
}