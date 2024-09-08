namespace Booking.Application.Booking.Queries.GetBookingsByUserId;

// Todo: Fix it for better optimization
public class GetBookingsByUserIdQueryHandler
    (IApplicationDbContext context)
    : IQueryHandler<GetBookingsByUserIdQuery, GetBookingsByUserIdResult>
{
    public async Task<GetBookingsByUserIdResult> Handle(GetBookingsByUserIdQuery query, CancellationToken cancellationToken)
    {
        var bookingsId = await context.Bookings
            .AsNoTracking()
            .Where(b => b.UserId.Value == query.UserId)
            .Select(b => b.Id)
            .ToListAsync(cancellationToken);
        
        var bookings = await context.Bookings
            .AsNoTracking()
            .Where(b => bookingsId.Contains(b.Id))
            .ToListAsync(cancellationToken);
        
        var bookingItems = await context.BookingItems
            .AsNoTracking()
            .Where(bi => bookingsId.Contains(bi.BookingId))
            .ToListAsync(cancellationToken);
        
        foreach (var booking in bookings)
        {
            var items = bookingItems.Where(bi => bi.BookingId.Value == booking.Id.Value).ToList();
            foreach (var item in items)
            {
                booking.Add(item.EventId, item.EventLocationId, item.EventName, item.Quantity, item.Price);
            }
        }
        
        var bookingDtos = bookings.ToBookingDtoList();
        
        return new GetBookingsByUserIdResult(bookingDtos);
    }
}