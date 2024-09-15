namespace EventBooking.Payment.Features.Checkout;

public class FullfilmentEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/fullfilment", async (
                IPaymentService<StripeCheckoutRequest, StripeCheckoutResponse> paymentService,
                HttpContext httpContext,
                CancellationToken cancellationToken = default) =>
            {
                var json = await new StreamReader(httpContext.Request.Body).ReadToEndAsync(cancellationToken);

                try
                {
                    var stripeEvent = EventUtility.ConstructEvent(
                        json,
                        httpContext.Request.Headers["Stripe-Signature"],
                        "whsec_ce88cf3fba8b47579449beae696c757180f5ec366e90dd355bb34d029dec06e6");

                    if (stripeEvent.Type is Events.CheckoutSessionCompleted
                        or Events.CheckoutSessionAsyncPaymentSucceeded)
                    {
                        var session = stripeEvent.Data.Object as Session;
                        await paymentService.FullfillCheckoutSessionAsync(session.Id, cancellationToken);
                        return Results.Ok(new { message = "Event processed successfully" });
                    }
                    else
                    {
                        return Results.Ok(new { message = "Received but not processed" });
                    }
                    
                    return Results.Ok();
                }
                catch (StripeException e)
                {
                    return Results.Problem(e.Message, statusCode: StatusCodes.Status400BadRequest);
                }
            });
    }
}