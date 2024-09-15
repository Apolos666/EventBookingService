namespace EventBooking.Payment.Infrastructures.Payment;

public interface IPaymentRequest
{
    string CustomerId { get; set; }
    string SuccessUrl { get; set; }
    string CancelUrl { get; set; }
}