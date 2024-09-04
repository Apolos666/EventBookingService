namespace EventBooking.Event.Features.GetEventById;

//public record GetEventByIdRequest(Guid EventId);

public record GetEventByIdResponse(Models.Event Event);

public class GetEventByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/events/{eventId:guid}", async (Guid eventId, ISender sender) =>
        {
            var query = new GetEventByIdQuery(eventId);

            var result = await sender.Send(query);

            var response = result.Adapt<GetEventByIdResponse>();

            return Results.Ok(response);
        })
        .WithName("GetEventById")
        .Produces<GetEventByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Event By Id")
        .WithDescription("Gets an event by its id.")
        .WithTags(nameof(Models.Event));
    }
}