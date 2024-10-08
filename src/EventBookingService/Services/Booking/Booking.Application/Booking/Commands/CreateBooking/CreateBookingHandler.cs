﻿namespace Booking.Application.Booking.Commands.CreateBooking;

public class CreateBookingCommandHandler
    (IApplicationDbContext context)
    : ICommandHandler<CreateBookingCommand, CreateBookingResult>
{
    public async Task<CreateBookingResult> Handle(CreateBookingCommand command, CancellationToken cancellationToken)
    {
        var booking = CreateNewBooking(command.Booking);
        
        context.Bookings.Add(booking);
        await context.SaveChangesAsync(cancellationToken);

        return new CreateBookingResult(booking.Id.Value);
    }

    private static BookingModel.Booking CreateNewBooking(BookingDto bookingDto)
    {
        var newBooking = BookingModel.Booking.Create(
            id: BookingId.Of(Guid.NewGuid()),
            userId: UserId.Of(bookingDto.UserId));

        foreach (var bookingItem in bookingDto.BookingItems)
        {
            newBooking.Add(
                DomainEventId.Of(bookingItem.EventId),
                bookingItem.StartDateTime,
                EventLocationId.Of(bookingItem.EventLocationId),
                eventLocatioName: bookingItem.EventLocationName,
                EventName.Of(bookingItem.EventName),
                quantity: bookingItem.Quantity,
                price: bookingItem.Price);
        }
        
        newBooking.ConfirmPayment();
        
        return newBooking;
    }
}