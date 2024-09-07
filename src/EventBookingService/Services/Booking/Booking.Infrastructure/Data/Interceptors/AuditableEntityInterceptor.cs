namespace Booking.Infrastructure.Data.Interceptors;

// Interceptor for auditing entity changes in the DbContext
public class AuditableEntityInterceptor
    ()
    : SaveChangesInterceptor
{
    // Synchronous method called when saving changes
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        // Update entities with audit information
        UpdateEntities(eventData.Context);
        // Call the base method to continue saving changes
        return base.SavingChanges(eventData, result);
    }

    // Asynchronous method called when saving changes
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        // Update entities with audit information
        UpdateEntities(eventData.Context);
        // Call the base method to continue saving changes asynchronously
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    // Method to update entities with audit information
    private static void UpdateEntities(DbContext? context)
    {
        // If context is null, return
        if (context == null) return;

        // Iterate through all tracked entities that implement IEntity
        foreach (var entry in context.ChangeTracker.Entries<IEntity>())
        {
            // If the entity is being added, set CreatedBy and CreatedAt
            if (entry.State == EntityState.Added)
            {
                // TODO: Get current user from HttpContext and set CreatedBy
                entry.Entity.CreatedBy = "Quang";
                entry.Entity.CreatedAt = DateTime.UtcNow;
            }

            // If the entity is being added or modified, or has changed owned entities, set LastModifiedBy and LastModified
            if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
            {
                entry.Entity.LastModifiedBy = "Quang";
                entry.Entity.LastModified = DateTime.UtcNow;
            }
        }
    }
}

// Extension methods for EntityEntry
public static class Extensions
{
    // Method to check if an entity has changed owned entities
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            r.TargetEntry.State is EntityState.Added or EntityState.Modified);
}