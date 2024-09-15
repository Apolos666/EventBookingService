namespace EventBooking.Payment.Infrastructures.Payment;

public interface IPaymentResponse
{
    string SessionId { get; set; }
}