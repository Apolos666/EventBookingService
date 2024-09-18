namespace EventBooking.Notification.Hubs;

public class NotificationHub
    (ILogger<NotificationHub> logger)
    : Hub<INotificationClient>
{
    public override Task OnConnectedAsync()
    {
        logger.LogInformation("Client connected: {connectionId}", Context.User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        logger.LogInformation("Client disconnected: {connectionId}", Context.ConnectionId);
        
        return base.OnDisconnectedAsync(exception);
    }
}