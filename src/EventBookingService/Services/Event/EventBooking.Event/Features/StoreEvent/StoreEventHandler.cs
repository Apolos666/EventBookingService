namespace EventBooking.Event.Features.StoreEvent;

public record StoreEventCommand(EventDto Event) : ICommand<StoreEventResult>;

public record StoreEventResult(Guid Id);

public class StoreEventHandler
    (IEventRepository repository, IPublishEndpoint publishEndpoint)
    : ICommandHandler<StoreEventCommand, StoreEventResult>
{
    public async Task<StoreEventResult> Handle(StoreEventCommand command, CancellationToken cancellationToken)
    {
        var eventId = await repository.StoreEventAsync(command.Event, cancellationToken);

        var createEvent = new CreateEvent
        {
            Description = command.Event.Description,
            Name = command.Event.Name,
            StartDateTime = command.Event.StartDateTime,
            EndDateTime = command.Event.EndDateTime,
        };
        
        await publishEndpoint.Publish(createEvent, cancellationToken);

        return new StoreEventResult(eventId);
    }
}