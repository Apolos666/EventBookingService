namespace Booking.Application.Dtos;

// Refactor: this record should be unique for each requirement 
public record BookingDto(
    Guid Id,
    Guid UserId,
    List<BookingItemDto> BookingItems);