namespace BuildingBlocks.Messaging.Events.Notifications;

public record CreateEvent : IntegrationEvent
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime StartDateTime { get; set; } = default!;
    public DateTime EndDateTime { get; set; } = default!;
}