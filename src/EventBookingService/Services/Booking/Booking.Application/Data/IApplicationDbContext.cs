namespace Booking.Application.Data;

public interface IApplicationDbContext
{
    IDbConnection Connection { get; }
    DatabaseFacade Database { get; }
    DbSet<BookingModel.Booking> Bookings { get; }
    DbSet<BookingModel.BookingItem> BookingItems { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}