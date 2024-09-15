﻿namespace EventBooking.Payment.Infrastructures.Payment;

public class StripePaymentService 
    : IPaymentService<StripeCheckoutRequest, StripeCheckoutResponse>
{
    private readonly IPublishEndpoint _publishEndpoint;

    public StripePaymentService(string apiKey, IPublishEndpoint publisherEndpoint)
    {
        StripeConfiguration.ApiKey = apiKey;
        _publishEndpoint = publisherEndpoint;
    }

    
    public async Task<StripeCheckoutResponse> CreateCheckoutSessionAsync(StripeCheckoutRequest request, CancellationToken cancellationToken)
    {
        var checkoutOptions = new SessionCreateOptions
        {
            LineItems = request.LineItems.ConvertAll(item => new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "USD",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = item.EventName,
                        Metadata = new Dictionary<string, string>()
                        {
                            {StripePaymentConstant.EventId, item.EventId},
                            {StripePaymentConstant.StartDateTime, item.StartDateTime},
                            {StripePaymentConstant.EventLocationId, item.EventLocationId}
                        }
                    },
                    UnitAmountDecimal = item.UnitAmount
                },
                Quantity = item.Quantity
            }),
            Metadata = new Dictionary<string, string>
            {
                { StripePaymentConstant.CustomerId, request.CustomerId },
            },
            Mode = "payment",
            SuccessUrl = request.SuccessUrl,
            CancelUrl = request.CancelUrl
        };
        
        var service = new SessionService();
        var session = await service.CreateAsync(checkoutOptions, cancellationToken: cancellationToken);
        
        return new StripeCheckoutResponse
        {
            SessionId = session.Id
        };
    }

    public async Task FullfillCheckoutSessionAsync(string sessionId, CancellationToken cancellationToken)
    {
        #region Retrieve checkout session

        var getSessionOptions = new SessionGetOptions
        {
            Expand = ["line_items.data.price.product"]
        };
        
        var service = new SessionService();
        var checkoutSession = await service.GetAsync(sessionId, getSessionOptions, cancellationToken: cancellationToken);

        #endregion

        #region Publish success checkout event

        var sucessCheckoutEvent = new SuccessCheckoutEvent
        {
            UserId = Guid.Parse(checkoutSession.Metadata[StripePaymentConstant.CustomerId]),
            CheckoutEventItems = checkoutSession.LineItems.Select(li => new CheckoutItemEvent
            {
                EventId = Guid.Parse(li.Price.Product.Metadata[StripePaymentConstant.EventId]),
                StartDateTime = DateTime.Parse(li.Price.Product.Metadata[StripePaymentConstant.StartDateTime]),
                EventLocationId = Guid.Parse(li.Price.Product.Metadata[StripePaymentConstant.EventLocationId]),
                EventName = li.Price.Product.Name,
                Quantity = (int)(li.Quantity ?? 0),
                Price = li.Price.UnitAmountDecimal ?? 0
            })
        };
        
        await _publishEndpoint.Publish(sucessCheckoutEvent, cancellationToken);

        #endregion
    }
}