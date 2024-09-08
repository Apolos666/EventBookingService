namespace Booking.Application.Booking.Queries.GetBookings;

// Todo: Fix it for better optimization
public class GetBookingsQueryHandler
    (IApplicationDbContext context)
    : IQueryHandler<GetBookingsQuery, GetBookingsResult>
{
    public async Task<GetBookingsResult> Handle(GetBookingsQuery query, CancellationToken cancellationToken)
    {
        var pageSize = query.PaginatedRequest.PageSize;
        var pageIndex = query.PaginatedRequest.PageIndex;
        
        var totalCount = await context.Bookings.LongCountAsync(cancellationToken);
        
        // Fetch booking IDs only, without the BookingItems (no circular reference)
        var bookingIds = await context.Bookings
            .AsNoTracking()
            .OrderBy(b => b.CreatedAt)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .Select(b => b.Id)
            .ToListAsync(cancellationToken);
        
        // Fetch bookings without BookingItems
        var bookings = await context.Bookings
            .AsNoTracking()
            .Where(b => bookingIds.Contains(b.Id))
            .ToListAsync(cancellationToken);
        
        // Fetch booking items separately
        var bookingItems = await context.BookingItems
            .AsNoTracking()
            .Where(bi => bookingIds.Contains(bi.BookingId))
            .ToListAsync(cancellationToken);

        
        // Associate BookingItems to their Bookings
        foreach (var booking in bookings)
        {
            var items = bookingItems.Where(bi => bi.BookingId.Value == booking.Id.Value).ToList();
            foreach (var item in items)
            {
                booking.Add(item.EventId, item.EventLocationId, item.EventName, item.Quantity, item.Price);
            }
        }
        
        var bookingDtos = bookings.ToBookingDtoList();
        
        var hasNextPage = totalCount > pageSize * (pageIndex + 1);
        var hasPreviousPage = bookings.Count != 0 && pageIndex > 0;

        return new GetBookingsResult(
            new PaginatedResult<BookingDto>(
                pageIndex,
                pageSize,
                totalCount,
                hasNextPage,
                hasPreviousPage,
                bookingDtos)
        );
    }
}