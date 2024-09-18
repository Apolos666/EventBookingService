namespace Booking.API.Endpoints;

// public record DeleteBookingRequest(Guid Id);

public record DeleteBookingResponse(bool IsSuccess);

public class DeleteBooking : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/booking/{id:guid}", async ([FromRoute] Guid id, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new DeleteBookingCommand(id));

            var response = result.Adapt<DeleteBookingResponse>();
            
            return Results.Ok(response);
        })
        .WithName("DeleteBooking") 
        .Produces<DeleteBookingResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete a booking")
        .WithDescription("Delete a booking")
        .WithTags(nameof(BookingModel.Booking))
        .RequireAuthorization();
    }
}