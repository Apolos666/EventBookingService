namespace Booking.Application.Dtos;

public record BookingItemDto(
    Guid BookingId,
    Guid EventId,
    Guid EventLocationId,
    string EventName,
    int Quantity,
    decimal Price);