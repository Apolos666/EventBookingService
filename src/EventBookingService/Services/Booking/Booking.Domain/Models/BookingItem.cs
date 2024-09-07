namespace Booking.Domain.Models;

public class BookingItem : Entity<BookingItemId> 
{
    public EventId EventId { get; private set; }
    public EventLocationId EventLocationId { get; private set; }   
    public EventName EventName { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
    
    public decimal TotalPrice { 
        get => Price * Quantity; 
        private set{} 
    }

    // EF Core
    public BookingItem()
    {
        
    }
    
    internal BookingItem(EventId eventId, EventLocationId eventLocationId, EventName eventName, int quantity, decimal price)
    {
        Id = BookingItemId.Of(Guid.NewGuid());
        EventId = eventId;
        EventLocationId = eventLocationId;
        EventName = eventName;
        Quantity = quantity;
        Price = price;
    }
}