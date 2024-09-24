namespace Booking.Application.Dtos;

// Refactor: this record should be unique for each requirement 
public record BookingItemDto(
    Guid BookingId,
    Guid EventId,
    DateTime StartDateTime,
    Guid EventLocationId,
    string EventLocationName,
    string EventName,
    int Quantity,
    decimal Price);