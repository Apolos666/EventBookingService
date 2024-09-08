namespace Booking.Application.Data;

public interface IApplicationDbContext
{
    DbSet<BookingModel.Booking> Bookings { get; }
    DbSet<BookingModel.BookingItem> BookingItems { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}