namespace Booking.Domain.Events;

public record BookingCreatedEvent(Models.Booking Booking) : IDomainEvent;