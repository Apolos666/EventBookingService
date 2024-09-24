namespace EventBooking.Event.Features.GetEventsUser;

// public record GetEventsUserRequest();

public record GetEventsUserResponse(IEnumerable<Models.Event> Events);

public class GetEventsUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/events/user", async (ISender sender) =>
            {
                var result = await sender.Send(new GetEventsUserQuery());

                var response = result.Adapt<GetEventsUserResponse>();

                return Results.Ok(response);
            })
            .WithName("GetEventsUser")
            .Produces<GetEventsUserResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Events User")
            .WithDescription("Gets a list of User's Event.")
            .WithTags(nameof(Models.Event))
            .RequireAuthorization();
    }
}