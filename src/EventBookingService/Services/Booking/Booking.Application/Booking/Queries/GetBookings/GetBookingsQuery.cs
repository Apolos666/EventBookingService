namespace Booking.Application.Booking.Queries.GetBookings;

public record GetBookingsQuery(PaginatedRequest PaginatedRequest) : IQuery<GetBookingsResult>;

public record GetBookingsResult(PaginatedResult<BookingDto> Bookings);