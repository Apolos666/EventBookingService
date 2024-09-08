namespace Booking.Application.Booking.Queries.GetBookingByUserId;

public class GetBookingByUserIdQueryHandler
    (IApplicationDbContext context)
    : IQueryHandler<GetBookingByUserIdQuery, GetBookingByUserIdResult>
{
    public async Task<GetBookingByUserIdResult> Handle(GetBookingByUserIdQuery query, CancellationToken cancellationToken)
    {
        var bookings = await context.Bookings
            .Include(b => b.BookingItems)
            .AsNoTracking()
            .Where(b => b.UserId.Value == query.UserId)
            .ToListAsync(cancellationToken);
        
        var bookingDtos = bookings.ToBookingDtoList();
        
        return new GetBookingByUserIdResult(bookingDtos);
    }
}