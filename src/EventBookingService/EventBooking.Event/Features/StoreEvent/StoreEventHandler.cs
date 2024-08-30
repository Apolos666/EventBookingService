namespace EventBooking.Event.Features.StoreEvent;

public record StoreEventCommand(EventDto Event) : ICommand<StoreEventResult>;

public record StoreEventResult(Guid Id);

public class StoreEventHandler
    (IDocumentSession session)
    : ICommandHandler<StoreEventCommand, StoreEventResult>
{
    public async Task<StoreEventResult> Handle(StoreEventCommand command, CancellationToken cancellationToken)
    {
        var @event = CreateNewEvent(command);

        session.Store(@event);
        await session.SaveChangesAsync(cancellationToken);

        return new StoreEventResult(@event.Id);
    }

    private static Models.Event CreateNewEvent(StoreEventCommand command)
    {
        var @event = Models.Event.Create(command.Event.Name, command.Event.Description, command.Event.StartDateTime, command.Event.EndDateTime);

        foreach (var eventLocationDto in command.Event.EventLocationDtos)
        {
            var location = Location.Create(
                eventLocationDto.Location.Name,
                eventLocationDto.Location.Address,
                eventLocationDto.Location.City,
                eventLocationDto.Location.State,
                eventLocationDto.Location.ZipCode,
                eventLocationDto.Location.Country);
            var eventLocation = EventLocation.Create(location, eventLocationDto.MaxAttendees);
            @event.AddEventLocation(eventLocation);
        }

        return @event;
    }
}