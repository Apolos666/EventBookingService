namespace Booking.API.Endpoints;

// public record GetBookingsByUserIdRequest(Guid UserId);

public record GetBookingsByUserIdResponse(IEnumerable<BookingDto> Bookings);

public class GetBookingsByUserId : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/booking/user", async ([FromServices] ISender sender, [FromServices] IUserIdentityAccessor userIdentityAccessor) =>
        {
            var query = new GetBookingsByUserIdQuery(Guid.Parse(userIdentityAccessor.UserId));
            
            var result = await sender.Send(query);
            
            var response = result.Adapt<GetBookingsByUserIdResponse>();
            
            return Results.Ok(response);
        })
        .WithName("GetBookingsByUserId")
        .Produces<GetBookingsByUserIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get bookings by user id")
        .WithDescription("Get bookings by user id")
        .WithTags(nameof(BookingModel.Booking))
        .RequireAuthorization();
    }
}