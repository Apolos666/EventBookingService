namespace EventBooking.Payment.Infrastructures.Payment;

public class StripeCheckoutRequest : IPaymentRequest
{
    public string CustomerId { get; set; }
    public List<StripeLineItem> LineItems { get; init; }
    public string SuccessUrl { get; set; }
    public string CancelUrl { get; set; }
}