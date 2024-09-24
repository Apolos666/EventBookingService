namespace EventBooking.Event.Features.GetEvents;

public record GetEventsRequest(int? PageNumber = 1, int? PageSize = 10);

public record GetEventsResponse(IEnumerable<Models.Event> Events);

public class GetEventsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/events", async ([AsParameters] GetEventsRequest request, ISender sender, HttpContext httpContext) =>
            {
                var dada = httpContext;

                var query = request.Adapt<GetEventsQuery>();

                var result = await sender.Send(query);

                var response = result.Adapt<GetEventsResponse>();

                return Results.Ok(response);
            })
            .WithName("GetEvents")
            .Produces<GetEventsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Events")
            .WithDescription("Gets a list of Events.")
            .WithTags(nameof(Models.Event))
            .AllowAnonymous();
    }
}