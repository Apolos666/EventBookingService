namespace EventBooking.Event.Models;

public sealed class EventLocation
{
    public Guid Id { get; private set; }
    public Location Location { get; private set; } =  default!;
    public int MaxAttendees { get; private init; }
    public int RegisteredAttendees { get; private set; }

    public static EventLocation Create(Location location, int maxAttendees)
    {
        var eventLocation = new EventLocation
        {
            Id = Guid.NewGuid(),
            Location = location,
            MaxAttendees = maxAttendees,
            RegisteredAttendees = 0
        };

        return eventLocation;
    }

    public void IncreaseRegisteredAttendees()
    {
        if (RegisteredAttendees < MaxAttendees)
            RegisteredAttendees++;
        else
            throw new EventLocationInvalidOperation(
                nameof(IncreaseRegisteredAttendees),
                "Cannot register more attendees than the maximum allowed."
            );
    }

    public void DecreaseRegisteredAttendees()
    {
        if (RegisteredAttendees > 0)
            RegisteredAttendees--;
        else
            throw new EventLocationInvalidOperation(
                nameof(DecreaseRegisteredAttendees),
                "Cannot unregister more attendees than the minimum allowed."
            );
    }

    public void UpdateLocation(Location location)
    {
        Location = location;
    }
}