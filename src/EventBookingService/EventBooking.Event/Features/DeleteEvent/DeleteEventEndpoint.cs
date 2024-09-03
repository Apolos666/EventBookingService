namespace EventBooking.Event.Features.DeleteEvent;

// public record DeleteEventRequest(Guid EventId);

public record DeleteEventResponse(bool IsSuccess);

public class DeleteEventEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/events/{eventId}", async (Guid eventId, ISender sender ) =>
        {
            var result = await sender.Send(new DeleteEventCommand(eventId));

            var response = result.Adapt<DeleteEventResponse>();

            return Results.Ok(response);
        })
        .WithName("DeleteEvent")
        .Produces<DeleteEventResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete Event")
        .WithDescription("Deletes an event.")
        .WithTags(nameof(Models.Event))
        .RequireAuthorization(nameof(EventBookingPolicy.UserOnly));
    }
}