﻿namespace Booking.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");
        
        // Add services to the container.
        service.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        service.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
        
        service.AddDbContext<ApplicationDbContext>((sp, config) =>
        {
            config.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            config.UseMySQL(connectionString!);
        });

        service.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        service.AddScoped<IApplicationWriteDbConnection, ApplicationWriteDbConnection>();
        service.AddScoped<IApplicationReadDbConnection, ApplicationReadDbConnection>();
        
        return service;
    }
}