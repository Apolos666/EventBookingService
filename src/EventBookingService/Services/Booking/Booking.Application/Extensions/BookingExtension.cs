﻿namespace Booking.Application.Extensions;

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
                EventLocationName: bookingItem.EventLocationName,
                EventName: bookingItem.EventName.Value,
                Quantity: bookingItem.Quantity,
                Price: bookingItem.Price
            )).ToList()));
    }
    
    // public static IEnumerable<GetBookingDto> ToGetBookingDtoList(this IEnumerable<BookingModel.Booking> bookings)
    // {
    //     return bookings.Select(booking => new GetBookingDto(
    //         Id: booking.Id.Value,
    //         UserId: booking.UserId.Value,
    //         CreatedAt: booking.CreatedAt.Value,
    //         BookingStatus: booking.BookingStatus,
    //         TotalQuantity: booking.TotalQuantity,
    //         TotalPrice: booking.TotalPrice,
    //         BookingItems: booking.BookingItems.Select(bookingItem => new GetBookingItemDto(
    //             BookingId: bookingItem.BookingId.Value,
    //             EventId: bookingItem.EventId.Value,
    //             StartDateTime: bookingItem.StartDateTime,
    //             EventLocationId: bookingItem.EventLocationId.Value,
    //             EventLocationName: bookingItem.EventLocationName,
    //             EventName: bookingItem.EventName.Value,
    //             Quantity: bookingItem.Quantity,
    //             Price: bookingItem.Price,
    //             TotalPrice: bookingItem.TotalPrice,
    //             Code: bookingItem.Code.Value
    //         )).ToList()));
    // }
}