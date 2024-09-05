namespace EventBooking.Discount.Services;

public class DiscountService
    (DiscountContext dbContext)
    : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext
            .Coupons
            .FirstOrDefaultAsync(c => c.EventName == request.EventName);
        
        if (coupon is null) 
            coupon = new Coupon { EventName = "No Discount", Description = "No Discount", DiscountPercentage = 0 };

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));
        
        dbContext.Coupons.Add(coupon);
        await dbContext.SaveChangesAsync();
        
        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }
    
    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));
        
        dbContext.Coupons.Update(coupon);
        await dbContext.SaveChangesAsync();
        
        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext
            .Coupons
            .FirstOrDefaultAsync(c => c.EventName == request.EventName);
        
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount with EventName={request.EventName} not found."));
        
        dbContext.Coupons.Remove(coupon);
        await dbContext.SaveChangesAsync();
        
        return new DeleteDiscountResponse { Success = true };
    }
}