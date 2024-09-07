namespace Booking.Domain.ValueObjects;

public class BookingId
{
    public Guid Value { get; }
    
    private BookingId(Guid value) => Value = value;
    
    public static BookingId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        
        if (value == Guid.Empty)
            throw new DomainException("Booking id cannot be empty.");

        return new BookingId(value);
    }
}