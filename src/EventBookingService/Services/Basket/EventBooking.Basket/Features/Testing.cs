namespace EventBooking.Basket.Features;

public class Testing : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/testing", (IHttpContextAccessor contextAccessor)  =>
        {
            var s = contextAccessor;
            
            return "Hello World!";

        }).RequireAuthorization();
    }
}