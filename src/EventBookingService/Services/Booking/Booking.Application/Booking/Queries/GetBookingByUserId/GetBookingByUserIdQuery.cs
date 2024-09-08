namespace Booking.Application.Booking.Queries.GetBookingByUserId;

public record GetBookingByUserIdQuery(Guid UserId) : IQuery<GetBookingByUserIdResult>;

public record GetBookingByUserIdResult(IEnumerable<BookingDto> Bookings);