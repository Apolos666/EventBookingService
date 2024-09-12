namespace EventBooking.Notification.Features.StartingSoonEventHandler;

public class StartingSoonEventHandler
    (ILogger<StartingSoonEventHandler> logger, IHubContext<NotificationHub, INotificationClient> hub)
    : IConsumer<StartingSoonEvent>
{
    public async Task Consume(ConsumeContext<StartingSoonEvent> context)
    {
        logger.LogInformation("Sending notification to user {UserId} for event {Name}", context.Message.UserId, context.Message.Name);
        
        const string title = "Event starting soon";
        var message = $"{context.Message.Name} about starting in {context.Message.StartDateTime.Millisecond}/{context.Message.StartDateTime.Minute}/{context.Message.StartDateTime.Hour}";

        var userIds = context.Message.UserId.Select(ui => ui.ToString()).ToList();
        await hub.Clients.Users(userIds).ReceiveNotification(title, message);
    }
}