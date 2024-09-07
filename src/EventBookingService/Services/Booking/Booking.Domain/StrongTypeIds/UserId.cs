namespace Booking.Domain.ValueObjects;

public class UserId
{
    public Guid Value { get; }
    
    private UserId(Guid value) => Value = value;
    
    public static UserId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        
        if (value == Guid.Empty)
            throw new DomainException("Event location id cannot be empty.");

        return new UserId(value);
    }
}