namespace Booking.Application.Extensions;

public static class BookingExtension
{
    public static IEnumerable<BookingDto> ToBookingDtoList(this IEnumerable<BookingModel.Booking> booking)
    {
        return booking.Select(booking => new BookingDto(
            Id: booking.Id.Value,
            UserId: booking.UserId.Value,
            BookingItems: booking.BookingItems.Select(bookingItem => new BookingItemDto(
                BookingId: bookingItem.BookingId.Value,
                EventId: bookingItem.EventId.Value,
                StartDateTime: bookingItem.StartDateTime,
                EventLocationId: bookingItem.EventLocationId.Value,
                EventName: bookingItem.EventName.Value,
                Quantity: bookingItem.Quantity,
                Price: bookingItem.Price
            )).ToList()));
    }
}