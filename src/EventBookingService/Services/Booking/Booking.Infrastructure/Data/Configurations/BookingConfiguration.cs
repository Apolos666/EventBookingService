namespace Booking.Infrastructure.Data.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<BookingModel.Booking>
{
    public void Configure(EntityTypeBuilder<BookingModel.Booking> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .HasConversion(
                bookingId => bookingId.Value, 
                dbId => BookingId.Of(dbId)
                );
        
        builder.Property(b => b.UserId)
            .HasConversion(
                userId => userId.Value,
                dbId => UserId.Of(dbId)
            );

        builder.HasMany(b => b.BookingItems)
            .WithOne()
            .HasForeignKey(bi => bi.BookingId);

        builder.Property(b => b.BookingStatus)
            .HasConversion(
                bookingStatus => bookingStatus.ToString(),
                bookingStatusDb => (BookingStatus)Enum.Parse(typeof(BookingStatus), bookingStatusDb)
            )
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(b => b.TotalQuantity);
        builder.Property(b => b.TotalPrice);
    }
}