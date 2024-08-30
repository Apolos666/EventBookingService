using EventBooking.Event.Data;

namespace EventBooking.Event.Features.StoreEvent;

public record StoreEventCommand(EventDto Event) : ICommand<StoreEventResult>;

public record StoreEventResult(Guid Id);

public class StoreEventHandler
    (IEventRepository repository)
    : ICommandHandler<StoreEventCommand, StoreEventResult>
{
    public async Task<StoreEventResult> Handle(StoreEventCommand command, CancellationToken cancellationToken)
    {
        var eventId = await repository.StoreEventAsync(command.Event, cancellationToken);

        return new StoreEventResult(eventId);
    }
}