namespace EventBooking.Discount.Data;

public class DiscountContext(DbContextOptions<DiscountContext> options) : DbContext(options)
{
    public DbSet<Coupon> Coupons { get; init; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().HasData(
            new Coupon { Id = 1, EventName = "Event 1", Description = "Event 1 Description", DiscountPercentage = 10 },
            new Coupon { Id = 2, EventName = "Event 2", Description = "Event 2 Description", DiscountPercentage = 20 },
            new Coupon { Id = 3, EventName = "Event 3", Description = "Event 3 Description", DiscountPercentage = 30 }
        );
    }
}