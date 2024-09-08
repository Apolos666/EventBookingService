namespace Booking.API.Endpoints;

// public record GetBookingsRequest(PaginatedRequest PaginatedRequest);

public record GetBookingsResponse(PaginatedResult<BookingDto> Bookings);

public class GetBookings : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/booking", async ([AsParameters] PaginatedRequest request, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new GetBookingsQuery(request));

            var response = result.Adapt<GetBookingsResponse>();

            return Results.Ok(response);
        })
        .WithName("GetBookings")
        .Produces<GetBookingsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get bookings")
        .WithDescription("Get bookings")
        .WithTags(nameof(BookingModel.Booking));
    }
}