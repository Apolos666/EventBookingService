namespace EventBooking.Payment.Infrastructures.Payment;

public interface IPaymentLineItem
{
    string EventName { get; set; }
    decimal UnitAmount { get; set; }
    int Quantity { get; set; }
}