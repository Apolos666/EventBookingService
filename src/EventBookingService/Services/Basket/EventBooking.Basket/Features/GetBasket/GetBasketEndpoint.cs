namespace EventBooking.Basket.Features.GetBasket;

// public record GetBasketRequest(Guid UserId)

public record GetBasketResponse(EventCart cart);

public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket", async (ISender sender) =>
        {
            var result = await sender.Send(new GetBasketQuery());

            var response = result.Adapt<GetBasketResponse>();

            return Results.Ok(response);
        })
        .WithName("GetBasket")
        .Produces<GetBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Basket")
        .WithDescription("Get the basket for the user.")
        .WithTags(nameof(EventCart))
        .RequireAuthorization(nameof(EventBookingPolicy.UserOnly));
    }
}