namespace EventBooking.Event.Models;


public sealed class Event
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public DateTime StartDateTime { get; private set; } = default!;
    public DateTime EndDateTime { get; private set; } = default!;
    private List<EventLocation> _eventLocations = [];
    public IReadOnlyList<EventLocation> EventLocations => _eventLocations;

    public static Event Create(string name, string description, DateTime startDateTime, DateTime endDateTime)
    {
        var @event = new Event
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            StartDateTime = startDateTime,
            EndDateTime = endDateTime
        };

        return @event;
    }

    public void AddEventLocation(EventLocation eventLocation)
    {
        _eventLocations.Add(eventLocation);
    }
    
    public void RemoveEventLocation(EventLocation eventLocation)
    {
        _eventLocations.Remove(eventLocation);
    }
}