namespace Booking.Infrastructure.Data.Extensions;

public class InitialData
{
    public static IEnumerable<BookingModel.Booking> BookingsWithItems
    {
        get
        {
            var booking1 = BookingModel.Booking.Create(
                BookingId.Of(Guid.Parse("a1b2c3d4-e5f6-4747-b8d9-00112233445a")),
                UserId.Of(Guid.Parse("0ce7c7ad-cbbe-4977-94a9-497406fb1507"))
            );

            booking1.Add(
                EventId.Of(Guid.Parse("11111111-2222-3333-4444-555555555555")),
                DateTime.Now.AddSeconds(30),
                EventLocationId.Of(Guid.Parse("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee")),
                "Event Location 1",
                EventName.Of("Event Name 1"),
                10,
                50
            );
            booking1.Add(
                EventId.Of(Guid.Parse("22222222-3333-4444-5555-666666666666")),
                DateTime.Now.AddSeconds(30),
                EventLocationId.Of(Guid.Parse("bbbbbbbb-cccc-dddd-eeee-ffffffffffff")),
                "Event Location 2",
                EventName.Of("Event Name 1"),
                10,
                50
            );

            var booking2 = BookingModel.Booking.Create(
                BookingId.Of(Guid.Parse("b2c3d4e5-f6a7-5858-c9e0-112233445566")),
                UserId.Of(Guid.Parse("ece528ef-dbb7-4967-a98c-4b89a19b57e5"))
            );

            booking2.Add(
                EventId.Of(Guid.Parse("33333333-4444-5555-6666-777777777777")),
                DateTime.Now.AddSeconds(30),
                EventLocationId.Of(Guid.Parse("cccccccc-dddd-eeee-ffff-aaaaaaaaaaaa")),
                "Event Location 3",
                EventName.Of("Event Name 2"),
                10,
                50
            );
            booking2.Add(
                EventId.Of(Guid.Parse("44444444-5555-6666-7777-888888888888")),
                DateTime.Now.AddSeconds(30),
                EventLocationId.Of(Guid.Parse("dddddddd-eeee-ffff-aaaa-bbbbbbbbbbbb")),
                "Event Location 4",
                EventName.Of("Event Name 2"),
                10,
                50
            );

            return new List<BookingModel.Booking> { booking1, booking2 };
        }
    }
}