namespace Booking.Domain.ValueObjects;

public class EventLocationId
{
    public Guid Value { get; }
    
    private EventLocationId(Guid value) => Value = value;
    
    public static EventLocationId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        
        if (value == Guid.Empty)
            throw new DomainException("Event location id cannot be empty.");

        return new EventLocationId(value);
    }
}