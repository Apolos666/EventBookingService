namespace EventBooking.Payment.Features.Checkout;

public class CheckoutEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/checkout", async
            (BasketProtoService.BasketProtoServiceClient basketProtoClient,
                IPaymentService<StripeCheckoutRequest, StripeCheckoutResponse> paymentService,
                IUserIdentityAccessor userIdentityAccessor,
                CancellationToken cancellationToken = default) =>
            {
                var eventCart = await basketProtoClient.GetEventCartAsync(
                    new GetEventCartRequest { UserId = userIdentityAccessor.UserId },
                    cancellationToken: cancellationToken);

                var checkoutRequest = new StripeCheckoutRequest
                {
                    CustomerId = userIdentityAccessor.UserId,
                    LineItems = eventCart.Items.Select(item => new StripeLineItem
                    {
                        EventName = item.EventName,
                        UnitAmount = (decimal)item.Price,
                        Quantity = item.Quantity,
                        EventId = item.EventId,
                        StartDateTime = item.StartDateTime,
                        EventLocationId = item.EventLocationId,
                        EventLocationName = item.EventLocationName
                    }).ToList(),
                    SuccessUrl = "http://localhost:3000/checkout/success",
                    CancelUrl = "http://localhost:3000/checkout/canceled"
                };
                
                var response = await paymentService.CreateCheckoutSessionAsync(checkoutRequest, cancellationToken);
                return Results.Ok(response.SessionId);
            })
            .WithName("Checkout")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Checkout")
            .WithDescription("Checkout")
            .WithTags("Checkout")
            .RequireAuthorization();
    }
}