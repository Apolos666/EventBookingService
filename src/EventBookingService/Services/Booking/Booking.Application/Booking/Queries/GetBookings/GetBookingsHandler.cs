namespace Booking.Application.Booking.Queries.GetBookings;

public class GetBookingsQueryHandler
    (IApplicationDbContext context)
    : IQueryHandler<GetBookingsQuery, GetBookingsResult>
{
    public async Task<GetBookingsResult> Handle(GetBookingsQuery query, CancellationToken cancellationToken)
    {
        var pageSize = query.PaginatedRequest.PageSize;
        var pageIndex = query.PaginatedRequest.PageIndex;
        
        var totalCount = await context.Bookings.LongCountAsync(cancellationToken);

        var bookings = await context.Bookings
            .Include(b => b.BookingItems)
            .AsNoTracking()
            .OrderBy(b => b.CreatedAt)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        
        var hasNextPage = totalCount > pageSize * (pageIndex + 1);
        var hasPreviousPage = bookings.Count != 0 && pageIndex > 0;

        return new GetBookingsResult(
            new PaginatedResult<BookingDto>(
                pageIndex,
                pageSize,
                totalCount,
                hasNextPage,
                hasPreviousPage,
                bookings.ToBookingDtoList())
        );
    }
}