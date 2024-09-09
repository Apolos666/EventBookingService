namespace EventBooking.Notification.Hubs;

public interface INotification
{
    Task ClientHook(Data data);
}

public record Data(int Id, string Message);

public class NotificationHub
    (ILogger<NotificationHub> logger)
    : Hub<INotification>
{
    public void ServerHook(Data data)
    {
        logger.LogInformation("ServerHook called with data: {data}", data);
    }

    public void PingAll()
    {
        logger.LogInformation("PingAll called");
        Clients.All.ClientHook(new Data(111, "PingAll"));
    }
    
    public void SelfPing()
    {
        logger.LogInformation("SelfPing called");
        Clients.Caller.ClientHook(new Data(222, "SelfPing"));
    }
    
    [HubMethodName("invocation_with_return")]
    public Data JustAFunction()
    {
        return new Data(1, "JustAFunction");
    }

    public override Task OnConnectedAsync()
    {
        logger.LogInformation("Client connected: {connectionId}", Context.ConnectionId);
        
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        logger.LogInformation("Client disconnected: {connectionId}", Context.ConnectionId);
        
        return base.OnDisconnectedAsync(exception);
    }
}