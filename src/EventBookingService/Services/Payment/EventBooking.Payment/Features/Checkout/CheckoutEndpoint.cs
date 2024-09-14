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

            StripeConfiguration.ApiKey =
                "sk_test_51PcP70RoqHqSv3QAWHTPNVCVkcxDwZ5L3MQM1zPTjjJMMvozvzcmJTOlBT1EW9XkEt51ozXRYfIPhcuQnZn0obhj00kvvEcFZp";

            var checkoutSessionOptions = new SessionCreateOptions
            {
                LineItems = eventCart.Items.Select(item => new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "USD",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.EventName,
                            Description = item.StartDateTime
                        },
                        UnitAmountDecimal = (decimal)item.Price
                    },
                    Quantity = item.Quantity
                }).ToList(),
                Mode = "payment",
                SuccessUrl = "http://localhost:3000/" + "?success=true",
                CancelUrl = "http://localhost:3000/" + "?canceled=true"
            };

            var service = new SessionService();
            var session = await service.CreateAsync(checkoutSessionOptions, cancellationToken: cancellationToken);
            
            return Results.Ok(session.Id);
        }).RequireAuthorization();
    }
}