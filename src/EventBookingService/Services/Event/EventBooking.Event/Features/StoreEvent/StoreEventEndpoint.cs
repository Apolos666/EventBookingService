namespace EventBooking.Event.Features.StoreEvent;

public record StoreEventRequest(EventDto Event);

public record StoreEventResponse(Guid Id);

public class StoreEventEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/events", async (StoreEventRequest request, ISender sender) =>
        {
            var command = request.Adapt<StoreEventCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<StoreEventResponse>();

            return Results.Created($"/events/{response.Id}", response);
        })
        .WithName("CreateEvent")
        .Produces<StoreEventResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Product")
        .WithDescription("Creates a new product.")
        .WithTags(nameof(Models.Event))
        .RequireAuthorization(nameof(EventBookingPolicy.UserOnly));
    }
}