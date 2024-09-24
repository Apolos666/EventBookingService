namespace Booking.Infrastructure.Data.Configurations;

public class BookingItemConfiguration : IEntityTypeConfiguration<BookingModel.BookingItem>
{
    public void Configure(EntityTypeBuilder<BookingModel.BookingItem> builder)
    {
        builder.HasKey(bi => bi.Id);
        
        builder.Property(bi => bi.Id)
            .HasConversion(
                bookingItemId => bookingItemId.Value,
                dbId => BookingItemId.Of(dbId)
            );
        
        builder.Property(bi => bi.BookingId)
            .HasConversion(
                bookingId => bookingId.Value,
                dbId => BookingId.Of(dbId)
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

        builder.Property(bi => bi.EventName)
            .HasConversion(
                eventName => eventName.Value,
                dbValue => EventName.Of(dbValue)
            );

        builder.Property(bi => bi.Quantity);
        
        builder.Property(bi => bi.Price);
        
        builder.Property(bi => bi.TotalPrice);

        builder.Property(bi => bi.Code)
            .HasConversion(
                code => code.Value,
                dbValue => ConfirmationCode.Of(dbValue)
            );
    }
}