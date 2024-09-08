namespace EventBooking.Basket.Features.CheckoutBasket;

//public record CheckoutBasketRequest();

public record CheckoutBasketResponse(bool IsSuccess);

public class CheckoutBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket/checkout", async (ISender sender) =>
        {
            var result = await sender.Send(new CheckoutBasketCommand());
            
            var response = result.Adapt<CheckoutBasketResponse>();
            
            return Results.Ok(response);
        })
        .WithName("CheckoutBasket")
        .Produces<CheckoutBasketResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest)
        .WithSummary("Checkout the basket")
        .WithDescription("Checkout the basket")
        .WithTags(nameof(EventCart))
        .RequireAuthorization(nameof(EventBookingPolicy.UserOnly));
    }
}