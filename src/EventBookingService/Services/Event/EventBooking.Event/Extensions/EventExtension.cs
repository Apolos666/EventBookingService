namespace EventBooking.Event.Extensions;

public static class EventExtension
{
    public static Models.Event ToEvent(this EventDto eventDto)
    {
        return new Models.Event
        {
            Name = eventDto.Name,
            Description = eventDto.Description,
            StartDateTime = eventDto.StartDateTime,
            EndDateTime = eventDto.EndDateTime,
            EventLocations = eventDto.EventLocationDtos.Select(el => el.ToEventLocation()).ToList()
        };
    }

    private static Models.EventLocation ToEventLocation(this EventLocationDto eventLocationDto)
    {
        return new Models.EventLocation
        {
            Location = eventLocationDto.Location.ToLocation(),
            MaxAttendees = eventLocationDto.MaxAttendees,
            Price = eventLocationDto.Price
        };
    }

    private static Models.Location ToLocation(this LocationDto locationDto)
    {
        return new Models.Location
        {
            Name = locationDto.Name,
            Address = locationDto.Address,
            City = locationDto.City,
            State = locationDto.State,
            ZipCode = locationDto.ZipCode,
            Country = locationDto.Country
        };
    }
}