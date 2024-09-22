namespace Booking.Infrastructure.Data.Configurations;

public class BookingItemConfiguration : IEntityTypeConfiguration<BookingModel.BookingItem>
{
    public void Configure(EntityTypeBuilder<BookingModel.BookingItem> builder)
    {
        // Configure the primary key for the BookingItem entity
        builder.HasKey(bi => bi.Id);
        
        // Configure the Id property with a custom conversion
        builder.Property(bi => bi.Id)
            .HasConversion(
                bookingItemId => bookingItemId.Value, // Convert BookingItemId to its underlying value
                dbId => BookingItemId.Of(dbId) // Convert the underlying value back to BookingItemId
            );
        
        builder.Property(bi => bi.EventId)
            .HasConversion(
                eventId => eventId.Value,
                dbId => EventId.Of(dbId)
            );

        builder.Property(bi => bi.StartDateTime);
        
        builder.Property(bi => bi.EventLocationId)
            .HasConversion(
                eventLocationId => eventLocationId.Value,
                dbId => EventLocationId.Of(dbId)
            );

        builder.Property(bi => bi.EventLocationName);

        // Configure the EventName property as a complex property
        builder.ComplexProperty(
            bi => bi.EventName, nameBuilder =>
            {
                nameBuilder.Property(n => n.Value)
                    .HasColumnName(nameof(BookingModel.BookingItem.EventName)) // Set the column name
                    .HasMaxLength(100) // Set the maximum length
                    .IsRequired(); // Make the property required
            });
        
        // Configure the Quantity property
        builder.Property(bi => bi.Quantity);
        
        // Configure the Price property
        builder.Property(bi => bi.Price);
        
        // Configure the TotalPrice property
        builder.Property(bi => bi.TotalPrice);
        
        builder.ComplexProperty(
            bi => bi.Code, codeBuilder =>
            {
                codeBuilder.Property(c => c.Value)
                    .HasColumnName(nameof(BookingModel.BookingItem.Code))
                    .IsRequired(false);
            });
    }
}