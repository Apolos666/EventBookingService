namespace Booking.Domain.ValueObjects;

public class EventId
{
    public Guid Value { get; }
    
    private EventId(Guid value) => Value = value;

    public static EventId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        
        if (value == Guid.Empty)
            throw new DomainException("Event id cannot be empty.");

        return new EventId(value);
    }
}