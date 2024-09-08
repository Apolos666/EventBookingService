namespace Booking.API.Endpoints;

public record CreateBookingRequest(BookingDto Booking);
public record CreateBookingResponse(Guid Id);

public class CreateBooking : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/booking", async ([FromBody] CreateBookingRequest request, [FromServices] ISender sender) =>
        {
            var command = request.Adapt<CreateBookingCommand>();
            
            var result = await sender.Send(command);

            var response = result.Adapt<CreateBookingResponse>();
            
            return Results.Created($"/booking/{result.Id}", response);
        })
        .WithName("CreateBooking")
        .Produces<CreateBookingResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create a new booking")
        .WithDescription("Create a new booking")
        .WithTags(nameof(BookingModel.Booking));
    }
}