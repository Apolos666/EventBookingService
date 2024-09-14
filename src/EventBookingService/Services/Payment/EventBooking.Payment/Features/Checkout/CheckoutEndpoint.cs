namespace EventBooking.Payment.Features.Checkout;

public class CheckoutEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/checkout", async (IClientCredentialsTokenManagementService tokenManagementService) =>
        {
            var token = await tokenManagementService.GetAccessTokenAsync("basket.client");
            
            return Results.Ok(token);
        }).RequireAuthorization();
    }
}