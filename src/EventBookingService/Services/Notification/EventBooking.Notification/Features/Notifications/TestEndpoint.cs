namespace EventBooking.Notification.Features.Notifications;

public class TestEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/send", async (IHubContext<NotificationHub, INotification> hub) =>
        {
            await hub.Clients.All.ClientHook(new Data(100, "Hello from TestEndpoint"));
        });
    }
}