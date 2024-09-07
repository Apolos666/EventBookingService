namespace Booking.Infrastructure.Data.Interceptors;

// Interceptor for dispatching domain events in the DbContext
public class DispatchDomainEventsInterceptor(IMediator mediator) : SaveChangesInterceptor
{
    // Synchronous method called when saving changes
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        // Dispatch domain events synchronously
        DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
        // Call the base method to continue saving changes
        return base.SavingChanges(eventData, result);
    }

    // Asynchronous method called when saving changes
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        // Dispatch domain events asynchronously
        await DispatchDomainEvents(eventData.Context);
        // Call the base method to continue saving changes asynchronously
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    // Method to dispatch domain events
    private async Task DispatchDomainEvents(DbContext? context)
    {
        // If context is null, return
        if (context == null) return;

        // Get all aggregates with domain events
        var aggregates = context.ChangeTracker
            .Entries<IAggregate>()
            .Where(a => a.Entity.DomainEvents.Any())
            .Select(a => a.Entity);

        // Get all domain events from aggregates
        var domainEvents = aggregates
            .SelectMany(a => a.DomainEvents)
            .ToList();

        // Clear domain events from aggregates
        aggregates.ToList().ForEach(a => a.ClearDomainEvents());

        // Publish each domain event
        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}