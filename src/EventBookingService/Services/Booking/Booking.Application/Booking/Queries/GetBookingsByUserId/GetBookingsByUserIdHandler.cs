using MassTransit.Util;

namespace Booking.Application.Booking.Queries.GetBookingsByUserId;

public class GetBookingsByUserIdQueryHandler(
    IApplicationReadDbConnection context,
    ILogger<GetBookingsByUserIdQueryHandler> logger)
    : IQueryHandler<GetBookingsByUserIdQuery, GetBookingsByUserIdResult>
{
    public async Task<GetBookingsByUserIdResult> Handle(GetBookingsByUserIdQuery query,
        CancellationToken cancellationToken)
    {
        const string sql = $"""
                              SELECT 
                                  b.Id as {nameof(GetBookingDto.Id)},
                                  b.UserId as {nameof(GetBookingDto.UserId)},
                                  b.CreatedAt as {nameof(GetBookingDto.CreatedAt)},
                                  b.BookingStatus as {nameof(GetBookingDto.BookingStatus)},
                                  b.TotalQuantity as {nameof(GetBookingDto.TotalQuantity)},
                                  b.TotalPrice as {nameof(GetBookingDto.TotalPrice)},
                                  bi.BookingId as {nameof(GetBookingItemDto.BookingId)},
                                  bi.EventId as {nameof(GetBookingItemDto.EventId)},
                                  bi.StartDateTime as {nameof(GetBookingItemDto.StartDateTime)},
                                  bi.EventLocationId as {nameof(GetBookingItemDto.EventLocationId)},
                                  bi.EventLocationName as {nameof(GetBookingItemDto.EventLocationName)},
                                  bi.EventName as {nameof(GetBookingItemDto.EventName)},
                                  bi.Quantity as {nameof(GetBookingItemDto.Quantity)},
                                  bi.Price as {nameof(GetBookingItemDto.Price)},
                                  bi.TotalPrice as {nameof(GetBookingItemDto.TotalPrice)},
                                  bi.Code as {nameof(GetBookingItemDto.Code)}
                              FROM Bookings as b
                              LEFT JOIN BookingItems as bi ON b.Id = bi.BookingId
                              WHERE b.UserId = @UserId
                            """;

        var bookingDictionary = new Dictionary<Guid, GetBookingDto>();

        var bookings = await context.QueryAsync<GetBookingDto, GetBookingItemDto, GetBookingDto>(
            sql,
            (getBooking, getBookingItem) =>
            {
                if (bookingDictionary.TryGetValue(getBooking.Id, out var existingBooking))
                {
                    getBooking = existingBooking;
                }
                else
                {
                    bookingDictionary.Add(getBooking.Id, getBooking);
                }
                
                getBooking.BookingItems.Add(getBookingItem);
                
                return getBooking;
            },
            new
            { 
                query.UserId
            },
            SplitOn: nameof(GetBookingItemDto.BookingId), 
            cancellationToken: cancellationToken
        );

        return new GetBookingsByUserIdResult(bookings.Distinct().ToList());
    }
}