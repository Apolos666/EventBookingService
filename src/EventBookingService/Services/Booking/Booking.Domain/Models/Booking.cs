namespace Booking.Domain.Models;

public class Booking : Aggregate<BookingId>
{
    public UserId UserId { get; private set; }
    public BookingStatus BookingStatus { get; private set; }
    public int TotalQuantity { 
        get => _bookingItems.Sum(x => x.Quantity); 
        private set {} 
    }
    public decimal TotalPrice { 
        get => _bookingItems.Sum(x => x.TotalPrice); 
        private set {} 
    }
    
    private readonly List<BookingItem> _bookingItems = [];
    public IReadOnlyList<BookingItem> BookingItems => _bookingItems.AsReadOnly();

    // EF Core
    public Booking()
    {
        
    }
    
    public static Booking Create(BookingId id, UserId userId)
    {
        var booking = new Booking()
        {
            Id = id,
            UserId = userId,
            BookingStatus = BookingStatus.Pending,
        };
        
        booking.AddDomainEvent(new BookingCreatedEvent(booking));
        
        return booking;
    }
    
    // public void Update();
    
    public void AddBookingItem(EventId eventId, EventLocationId eventLocationId, EventName eventName, int quantity, decimal price)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
        
        var bookingItem = new BookingItem(eventId, eventLocationId, eventName, quantity, price);
        _bookingItems.Add(bookingItem);
    }
    
    public void RemoveBookingItem(BookingItemId bookingItemId)
    {
        var bookingItem = _bookingItems.FirstOrDefault(x => x.Id == bookingItemId);
        
        if (bookingItem is null)
            throw new DomainException("Booking item not found.");
        
        _bookingItems.Remove(bookingItem);
    }
}