namespace Booking.Domain.Events;

public record BookingUpdatedEvent(Models.Booking Booking) : IDomainEvent;