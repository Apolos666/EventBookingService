using Booking.Infrastructure.Data;

namespace Booking.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");
        
        // Add services to the container.

        service.AddDbContext<ApplicationDbContext>((sp, config) =>
        {
            // Add Interceptors
            config.UseMySQL(connectionString);
        });
        
        return service;
    }
}