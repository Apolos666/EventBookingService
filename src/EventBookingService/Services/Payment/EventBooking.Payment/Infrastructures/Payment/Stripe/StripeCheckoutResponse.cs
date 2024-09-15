namespace EventBooking.Payment.Infrastructures.Payment;

public class StripeCheckoutResponse : IPaymentResponse
{
    public string SessionId { get; set; }
}