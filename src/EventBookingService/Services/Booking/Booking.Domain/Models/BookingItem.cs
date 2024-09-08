namespace Booking.Domain.Models;

public class BookingItem : Entity<BookingItemId> 
{
    public BookingId BookingId { get; private set; } = default!;
    public EventId EventId { get; private set; } = default!;
    public EventLocationId EventLocationId { get; private set; } = default!;   
    public EventName EventName { get; private set; } = default!;
    public int Quantity { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;
    
    public decimal TotalPrice { 
        get => Price * Quantity; 
        private set{} 
    }

    public ConfirmationCode Code { get; private set; } = default!;

    // EF Core
    public BookingItem()
    {
        
    }
    
    internal BookingItem(BookingId bookingId ,EventId eventId, EventLocationId eventLocationId, EventName eventName, int quantity, decimal price)
    {
        Id = BookingItemId.Of(Guid.NewGuid());
        BookingId = bookingId;
        EventId = eventId;
        EventLocationId = eventLocationId;
        EventName = eventName;
        Quantity = quantity;
        Price = price;
        Code = ConfirmationCode.Of("------");
    }
    
    public void GenerateConfirmationCode()
    {
        Code = ConfirmationCode.Generate();
    }
}