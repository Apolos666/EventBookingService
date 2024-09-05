namespace EventBooking.Discount.Models;

public class Coupon
{
    public int Id { get; set; }
    public string EventName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int DiscountPercentage { get; set; }
}