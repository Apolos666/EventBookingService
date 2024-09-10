namespace EventBooking.Notification.Hubs;

public class TestingEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/test", () => Results.Ok("Oke")).RequireAuthorization();
    }
}