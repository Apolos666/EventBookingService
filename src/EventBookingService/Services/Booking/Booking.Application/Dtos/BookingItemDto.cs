namespace Booking.Application.Dtos;

public record BookingItemDto(
    Guid BookingId,
    Guid EventId,
    DateTime StartDateTime,
    Guid EventLocationId,
    string EventLocationName,
    string EventName,
    int Quantity,
    decimal Price);