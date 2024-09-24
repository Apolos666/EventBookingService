namespace Booking.Application.Booking.Queries.GetBookingsByUserId;

public record GetBookingsByUserIdQuery(Guid UserId) : IQuery<GetBookingsByUserIdResult>;

public record GetBookingsByUserIdResult(IEnumerable<GetBookingDto> Bookings);