namespace EventBooking.Event.Features.StoreEvent;

public record StoreEventRequest(EventDto Event)
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    public static async ValueTask<StoreEventRequest> BindAsync(HttpContext httpContext, ParameterInfo parameterInfo)
    {
        if (!httpContext.Request.HasFormContentType)
        {
            throw new InvalidOperationException("Request content type is not multipart/form-data.");
        }

        var form = await httpContext.Request.ReadFormAsync();
        var eventJson = form["event"].ToString();

        var eventDto = JsonSerializer.Deserialize<EventDto>(eventJson, JsonSerializerOptions);

        if (eventDto is null)
        {
            throw new InvalidOperationException("Failed to deserialize EventDto.");
        }

        var imageFile = form.Files["Image"];
        eventDto = eventDto with { EventImage = imageFile };

        return new StoreEventRequest(eventDto);
    }
};

public record StoreEventResponse(Guid Id);

public class StoreEventEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/events", async (StoreEventRequest request, ISender sender) =>
        {
            var command = new StoreEventCommand(request.Event);

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

