namespace EventBooking.Event.Features.EventStartDateChecker;

public class EventStartDateChecker(
    ILogger<EventStartDateChecker> logger,
    IServiceScopeFactory serviceScopeFactory)
    : BackgroundService
{
    // Prefix for cache keys to prevent duplicate notifications
    private const string EVENT_NOTIFICATION_PREFIX = "event_being_notificated_";
    // Time interval between each check for upcoming events
    private static readonly TimeSpan _period = TimeSpan.FromMinutes(1);
    // Time window to check for events that are about to start
    private static readonly TimeSpan _eventCheckWindow = TimeSpan.FromMinutes(5);
    // Time duration to prevent duplicate notifications
    private static readonly TimeSpan _preventDuplicateNotification = TimeSpan.FromMinutes(6);
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using PeriodicTimer timer = new(_period);

        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
        {
            logger.LogInformation("UtcNow: {UtcNow}", DateTimeOffset.UtcNow);

            try
            {
                // Check and notify upcoming events
                await CheckAndNotifyUpcomingEvents(stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while checking for upcoming events");
            }
        }
    }

    // Checks for events that are about to start and sends notifications
    private async Task CheckAndNotifyUpcomingEvents(CancellationToken stoppingToken)
    {
        logger.LogInformation("Checking for events starting in the next {Minutes} minutes...",
            _eventCheckWindow.TotalMinutes);

        using var scope = serviceScopeFactory.CreateScope();
        var session = scope.ServiceProvider.GetRequiredService<IDocumentSession>();
        var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();
        var cache = scope.ServiceProvider.GetRequiredService<IDistributedCache>();

        // Retrieve upcoming events
        var eventsAboutToStart = await GetUpcomingEvents(session, stoppingToken);

        logger.LogInformation("Found {Count} events starting in the next {Minutes} minutes", eventsAboutToStart.Count,
            _eventCheckWindow.TotalMinutes);

        if (!eventsAboutToStart.Any()) return;

        // Send notifications for each event
        var notificationTasks = eventsAboutToStart.Select(e => NotifyEvent(e, publishEndpoint, cache, stoppingToken));
        await Task.WhenAll(notificationTasks);
    }

    // Sends a notification for a specific event if it has not already been notified
    private async Task NotifyEvent(Models.Event @event, IPublishEndpoint publishEndpoint, IDistributedCache cache, CancellationToken stoppingToken)
    {
        var cacheKey = $"{EVENT_NOTIFICATION_PREFIX}{@event.Id}";
        var isNotified = await cache.GetStringAsync(cacheKey, stoppingToken);
        if (!string.IsNullOrWhiteSpace(isNotified))
        {
            logger.LogInformation("Event {EventId} has already been notified", @event.Id);
            return;
        }

        var startingSoonEvent = CreateStartingSoonEvent(@event);
        await publishEndpoint.Publish(startingSoonEvent, stoppingToken);
        logger.LogInformation("Notification sent for event: {EventId}", @event.Id);

        await cache.SetStringAsync(cacheKey, "true", new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = _preventDuplicateNotification
        }, token: stoppingToken);
    }

    // Retrieves events that are about to start within the specified time window
    private static async Task<IReadOnlyList<Models.Event>> GetUpcomingEvents(IDocumentSession session, CancellationToken stoppingToken)
    {
        var now = DateTimeOffset.UtcNow;
        var endCheckWindow = now.Add(_eventCheckWindow);

        return await session.Query<Models.Event>()
            .Where(e => e.StartDateTime >= now && e.StartDateTime <= endCheckWindow)
            .ToListAsync(token: stoppingToken);
    }
    
    private static StartingSoonEvent CreateStartingSoonEvent(Models.Event e) =>
        new()
        {
            Description = e.Description,
            Name = e.Name,
            StartDateTime = e.StartDateTime,
            UserId = e.UserRegistedId
        };
}