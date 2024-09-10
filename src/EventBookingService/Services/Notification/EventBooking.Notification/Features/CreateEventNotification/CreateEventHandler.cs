namespace EventBooking.Notification.Features.CreateEventNotification;

public class CreateEventHandler
    (IHubContext<NotificationHub, INotificationClient> hub, ILogger<CreateEventHandler> logger)
    : IConsumer<CreateEvent>
{
    public async Task Consume(ConsumeContext<CreateEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);
        
        // Send notification to all users
        const string title = "New Event!!!";
        var message = $"{context.Message.Name} has been created, starting on {context.Message.StartDateTime} and ending on {context.Message.EndDateTime}, {context.Message.Description}";
        
        await hub.Clients.All.ReceiveNotification(title, message);
    }
}