namespace EventBooking.Event.Features;

public sealed record EventDto(
    string Name, 
    string Description,
    string RefundPolicy,
    string AboutThisEvent,
    DateTime StartDateTime, 
    DateTime EndDateTime,
    IFormFile EventImage,
    List<EventLocationDto> EventLocationDtos);

public sealed record EventLocationDto(LocationDto Location, int MaxAttendees, decimal Price);

public sealed record LocationDto(string Name, string Address, string City, string State, string ZipCode, string Country);

