namespace EventBooking.Event.Features.EventStartDateChecker;

public class EventStartDateChecker(
    ILogger<EventStartDateChecker> logger,
    IServiceScopeFactory serviceScopeFactory)
    : BackgroundService
{
    private static readonly TimeSpan _period = TimeSpan.FromMinutes(1);
    private static readonly TimeSpan _eventCheckWindow = TimeSpan.FromMinutes(5);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using PeriodicTimer timer = new(_period);

        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                await CheckAndNotifyUpcomingEvents(stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while checking for upcoming events");
            }
        }
    }

    private async Task CheckAndNotifyUpcomingEvents(CancellationToken stoppingToken)
    {
        logger.LogInformation("Checking for events starting in the next {Minutes} minutes...",
            _eventCheckWindow.TotalMinutes);

        using var scope = serviceScopeFactory.CreateScope();
        var session = scope.ServiceProvider.GetRequiredService<IDocumentSession>();
        var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

        var now = DateTimeOffset.UtcNow;
        var eventAboutToStart = await GetUpcomingEvents(session, now, stoppingToken);

        logger.LogInformation("Found {Count} events starting in the next {Minutes} minutes", eventAboutToStart.Count,
            _eventCheckWindow.TotalMinutes);

        if (eventAboutToStart.Any())
        {
            var startingSoonEvents = eventAboutToStart.Select(CreateStartingSoonEvent);
            await publishEndpoint.Publish(startingSoonEvents, stoppingToken);
        }
    }

    private static async Task<IReadOnlyList<Models.Event>> GetUpcomingEvents(IDocumentSession session, DateTimeOffset now, CancellationToken stoppingToken)
    {
        var fiveMinutesFromNow = now.AddMinutes(5);
        var tenMinutesFromNow = now.AddMinutes(10);

        return await session.Query<Models.Event>()
            .Where(e => e.StartDateTime >= fiveMinutesFromNow && e.StartDateTime <= tenMinutesFromNow)
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