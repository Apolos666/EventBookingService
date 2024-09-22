namespace EventBooking.Basket.Features.StoreBasket;

public record StoreBasketRequest(EventCartDto Cart);

public record StoreBasketResponse(EventCart Cart);

public class StoreBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/baskets", async (StoreBasketRequest request, ISender sender) =>
        {
            var command = request.Adapt<StoreBasketCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<StoreBasketResponse>();
            
            return Results.Ok(response);
        })
        .WithName("StoreBasket")
        .Produces<StoreBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Store Basket")
        .WithDescription("Store the basket for the user.")
        .WithTags(nameof(EventCart))
        .RequireAuthorization(nameof(EventBookingPolicy.UserOnly));
    }
}


