namespace Booking.Application.Booking.Queries.GetBookingsByUserId;

public class GetBookingsByUserIdQueryHandler
    (IApplicationDbContext context, ILogger<GetBookingsByUserIdQueryHandler> logger)
    : IQueryHandler<GetBookingsByUserIdQuery, GetBookingsByUserIdResult>
{
    public async Task<GetBookingsByUserIdResult> Handle(GetBookingsByUserIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get bookings by user id {query.UserId}", query.UserId);
        
        var userIdValue = UserId.Of(query.UserId);

        var bookings = await context.Bookings
            .AsNoTracking()
            .Where(b => b.UserId == userIdValue)
            .ToListAsync(cancellationToken);
        
        var bookingIds = bookings.Select(b => b.Id).ToList();
        
        var bookingItems = await context.BookingItems
            .AsNoTracking()
            .Where(bi => bookingIds.Contains(bi.BookingId))
            .ToListAsync(cancellationToken);
        
        foreach (var booking in bookings)
        {
            var items = bookingItems.Where(bi => bi.BookingId == booking.Id).ToList();
            foreach (var item in items)
            {
                booking.Add(item.EventId, item.StartDateTime, item.EventLocationId, item.EventName, item.Quantity, item.Price);
            }
        }
        
        var bookingDtos = bookings.ToBookingDtoList();
        
        return new GetBookingsByUserIdResult(bookingDtos);
    }
}