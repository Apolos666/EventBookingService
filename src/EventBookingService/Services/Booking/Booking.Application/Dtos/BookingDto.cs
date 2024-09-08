namespace Booking.Application.Dtos;

public record BookingDto(
    Guid Id,
    Guid UserId,
    List<BookingItemDto> BookingItems);