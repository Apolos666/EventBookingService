namespace Booking.Domain.ValueObjects;

public class BookingItemId
{
    public Guid Value { get; }
    
    private BookingItemId(Guid value) => Value = value;

    public static BookingItemId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        
        if (value == Guid.Empty)
            throw new DomainException("Booking item id cannot be empty.");

        return new BookingItemId(value);
    }
}