namespace BuildingBlocks.Messaging.Events.Notifications;

public record StartingSoonEvent : IntegrationEvent
{
    public List<Guid> UserId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDateTime { get; set; }
}