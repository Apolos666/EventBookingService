namespace Booking.Domain.Models;

public class BookingItem : Entity<BookingItemId> 
{
    internal BookingItem(BookingId bookingId , EventId eventId, DateTime startDateTime, EventLocationId eventLocationId, string eventLocationName, EventName eventName, int quantity, decimal price)
    {
        Id = BookingItemId.Of(Guid.NewGuid());
        BookingId = bookingId;
        EventId = eventId;
        StartDateTime = startDateTime;
        EventLocationId = eventLocationId;
        EventLocationName = eventLocationName;
        EventName = eventName;
        Quantity = quantity;
        Price = price;
        Code = ConfirmationCode.Of("------");
    }
    
    // Todo: This is garbage. Refactor this to use a dapper
    internal BookingItem(BookingItemId id, BookingId bookingId, EventId eventId, DateTime startDateTime, EventLocationId eventLocationId, string eventLocationName, EventName eventName, int quantity, decimal price, ConfirmationCode code)
    {
        Id = id;
        BookingId = bookingId;
        EventId = eventId;
        StartDateTime = startDateTime;
        EventLocationId = eventLocationId;
        EventLocationName = eventLocationName;
        EventName = eventName;
        Quantity = quantity;
        Price = price;
        Code = code;
    }
    
    public BookingId BookingId { get; private set; } = default!;
    public EventId EventId { get; private set; } = default!;
    public DateTime StartDateTime { get; init; } = default!;
    public EventLocationId EventLocationId { get; private set; } = default!;
    public string EventLocationName { get; set; }
    public EventName EventName { get; private set; } = default!;
    public int Quantity { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;
    
    public decimal TotalPrice { 
        get => Price * Quantity; 
        private set{} 
    }

    public ConfirmationCode Code { get; private set; } = default!;
    
    public void GenerateConfirmationCode()
    {
        Code = ConfirmationCode.Generate();
    }
}