namespace Booking.Domain.Models;

public class Booking : Aggregate<BookingId>
{
    private readonly List<BookingItem> _bookingItems = [];
    public IReadOnlyList<BookingItem> BookingItems => _bookingItems.AsReadOnly();
    
    public UserId UserId { get; private set; } = default!;
    public BookingStatus BookingStatus { get; private set; } = BookingStatus.Pending;
    public int TotalQuantity { 
        get => _bookingItems.Sum(x => x.Quantity); 
        private set {} 
    }

    public decimal TotalPrice
    {
        get => _bookingItems.Sum(x => x.TotalPrice);
        private set { }
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
    
    public void Add(EventId eventId, DateTime startDateTime, EventLocationId eventLocationId, string eventLocatioName, EventName eventName, int quantity, decimal price)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
        
        var bookingItem = new BookingItem(Id, eventId, startDateTime, eventLocationId, eventLocatioName, eventName, quantity, price);
        _bookingItems.Add(bookingItem);
    }
    
    // Todo: This is garbage. Refactor this to use a dapper
    public void Add(BookingItemId id, BookingId bookingId, EventId eventId, DateTime startDateTime, EventLocationId eventLocationId, string eventLocationName, EventName eventName, int quantity, decimal price, ConfirmationCode code)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
        
        var bookingItem = new BookingItem(id, bookingId, eventId, startDateTime, eventLocationId, eventLocationName, eventName, quantity, price, code);
        _bookingItems.Add(bookingItem);
    }
    
    public void Remove(BookingItemId bookingItemId)
    {
        var bookingItem = _bookingItems.FirstOrDefault(x => x.Id == bookingItemId);
        
        if (bookingItem is null)
            throw new DomainException("Booking item not found.");
        
        _bookingItems.Remove(bookingItem);
    }
    
    public void ConfirmPayment()
    {
        if (BookingStatus != BookingStatus.Pending)
            throw new DomainException("Cannot confirm payment for a booking that is not pending.");

        BookingStatus = BookingStatus.Confirmed;
        
        foreach (var bookingItem in _bookingItems)
        {
            bookingItem.GenerateConfirmationCode();
        }
    }
}