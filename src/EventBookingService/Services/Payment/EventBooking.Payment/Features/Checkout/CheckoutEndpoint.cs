namespace EventBooking.Payment.Features.Checkout;

public class CheckoutEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/checkout", async 
            (BasketProtoService.BasketProtoServiceClient basketProtoClient, 
                IUserIdentityAccessor userIdentityAccessor, 
                CancellationToken cancellationToken = default) =>
        {
            var eventCart = await basketProtoClient.GetEventCartAsync(new GetEventCartRequest {UserId = userIdentityAccessor.UserId}, cancellationToken: cancellationToken);
            
            return Results.Ok(eventCart);
        }).RequireAuthorization();
    }
}