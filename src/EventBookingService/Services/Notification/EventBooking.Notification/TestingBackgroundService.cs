namespace EventBooking.Notification;

public class TestingBackgroundService
    (IServiceScopeFactory serviceScopeFactory, ILogger<TestingBackgroundService> logger)
    : BackgroundService
{
    // Prefix for cache keys to prevent duplicate notifications
    private const string EVENT_NOTIFICATION_PREFIX = "event_being_notificated_";
    // Time interval between each check for upcoming events
    private static readonly TimeSpan _period = TimeSpan.FromSeconds(2);
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using PeriodicTimer timer = new(_period);
        using var scope = serviceScopeFactory.CreateScope();
        var hubcontext = scope.ServiceProvider.GetRequiredService<IHubContext<NotificationHub, INotificationClient>>();
        

        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
        {
            logger.LogInformation("UtcNow: {UtcNow}", DateTimeOffset.UtcNow);
            
            const string title = "New Event!!!";
            var message = $"Event has been created, starting on {DateTime.Now} and ending on {DateTime.Now.AddHours(1)}, Description";
            await hubcontext.Clients.All.ReceiveNotification(title, message);
        }
    }
}