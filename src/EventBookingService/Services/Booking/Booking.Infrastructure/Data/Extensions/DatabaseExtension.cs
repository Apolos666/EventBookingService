namespace Booking.Infrastructure.Data.Extensions;

public static class DatabaseExtension
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        await context.Database.MigrateAsync();
        
        await SeedData(context);
    }
    
    private static async Task SeedData(ApplicationDbContext context)
    {
        await SeedBookingsWithItems(context);
    }

    private static async Task SeedBookingsWithItems(ApplicationDbContext context)
    {
        if (!await context.Bookings.AnyAsync())
        {
            await context.Bookings.AddRangeAsync(InitialData.BookingsWithItems);
            await context.SaveChangesAsync();
        }
    }
}