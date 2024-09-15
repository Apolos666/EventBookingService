namespace EventBooking.Payment.Infrastructures.Payment;

public interface IPaymentService<in TRequest, TResponse>
    where TRequest : IPaymentRequest
    where TResponse : IPaymentResponse
{
    Task<TResponse> CreateCheckoutSessionAsync(TRequest request, CancellationToken cancellationToken);
    Task FullfillCheckoutSessionAsync(string sessionId, CancellationToken cancellationToken);
}