using EventBooking.Event.Models;

namespace EventBooking.Event.Features;

public sealed record EventDto(string Name, string Description, DateTime StartDateTime, DateTime EndDateTime, List<EventLocationDto> EventLocationDtos);

public sealed record EventLocationDto(LocationDto Location, int MaxAttendees, decimal Price);

public sealed record LocationDto(string Name, string Address, string City, string State, string ZipCode, string Country);

