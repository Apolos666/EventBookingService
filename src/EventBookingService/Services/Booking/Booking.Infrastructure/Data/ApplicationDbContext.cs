namespace Booking.Infrastructure.Data;

public class ApplicationDbContext
    (DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<BookingModel.Booking> Bookings => Set<BookingModel.Booking>();
    public DbSet<BookingModel.BookingItem> BookingItems => Set<BookingModel.BookingItem>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}