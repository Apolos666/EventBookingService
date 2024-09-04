using EventBooking.Event.Data;

namespace EventBooking.Event.Features.DeleteEvent;

public record DeleteEventCommand(Guid EventId) : ICommand<DeleteEventResult>;

public record DeleteEventResult(bool IsSuccess);

public class DeleteEventHandler
    (IEventRepository eventRepository)
    : ICommandHandler<DeleteEventCommand, DeleteEventResult>
{
    public async Task<DeleteEventResult> Handle(DeleteEventCommand command, CancellationToken cancellationToken)
    {
        var result = await eventRepository.DeleteEventAsync(command.EventId, cancellationToken);
        return new DeleteEventResult(result);
    }
}