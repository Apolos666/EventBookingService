namespace BuildingBlocks.Messaging.Events.Bookings;

public record BookingCreatedEvent : IntegrationEvent
{
    public List<Guid> EventId { get; set; } = default!;
    public Guid UserId { get; set; } = default!;
}