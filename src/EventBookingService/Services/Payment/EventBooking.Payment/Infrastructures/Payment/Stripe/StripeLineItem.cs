namespace EventBooking.Payment.Infrastructures.Payment;

public class StripeLineItem : IPaymentLineItem
{
    public string EventId { get; set; }
    public string StartDateTime { get; set; }
    public string EventLocationId { get; set; }
    public string EventLocationName { get; set; }
    public string EventName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitAmount { get; set; }
}