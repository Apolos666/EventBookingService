namespace EventBooking.Event.Models;

public sealed class Location
{
    public string Name { get; private set; } = default!;
    public string Address { get; private set; } = default!;
    public string City { get; private set; } = default!;
    public string State { get; private set; } = default!;
    public string ZipCode { get; private set; } = default!;
    public string Country { get; private set; } = default!;

    public static Location Create(string name, string address, string city, string state, string zipCode, string country)
    {
        var location = new Location
        {
            Name = name,
            Address = address,
            City = city,
            State = state,
            ZipCode = zipCode,
            Country = country
        };

        return location;
    }
}