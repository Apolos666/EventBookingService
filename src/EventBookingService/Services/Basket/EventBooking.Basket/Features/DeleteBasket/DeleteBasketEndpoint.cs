namespace EventBooking.Basket.Features.DeleteBasket;

// public record DeleteBasketRequest(Guid Id);

public record DeleteBasketResponse(bool IsSuccess);

public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/baskets", async (ISender sender) =>
        {
            var result = await sender.Send(new DeleteBasketCommand());

            var response = result.Adapt<DeleteBasketResponse>();

            return Results.Ok(response);
        })
        .WithName("DeleteBasket")
        .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete Basket")
        .WithDescription("Delete the basket for the user.")
        .WithTags(nameof(EventCart))
        .RequireAuthorization(nameof(EventBookingPolicy.UserOnly));
    }
}