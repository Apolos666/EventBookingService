namespace EventBooking.Event.Models;

public sealed class Event
{
    public Guid Id { get; set; }
    public Guid HostId { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime StartDateTime { get; set; } = default!;
    public DateTime EndDateTime { get; set; } = default!;
    public List<EventLocation> EventLocations { get; set; } = [];
    public List<Guid> UserRegistedId { get; set; } = [];
}