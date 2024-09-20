namespace EventBooking.Event.Features.StoreEvent;

public record StoreEventCommand(EventDto Event) : ICommand<StoreEventResult>;

public record StoreEventResult(Guid Id);

public class StoreEventHandler
    (IEventRepository repository, IPublishEndpoint publishEndpoint, ImageStorage.ImageStorageClient imageStorageClient)
    : ICommandHandler<StoreEventCommand, StoreEventResult>
{
    public async Task<StoreEventResult> Handle(StoreEventCommand command, CancellationToken cancellationToken)
    {
        var imageUrl = await UploadImageAsync(command.Event.EventImage, cancellationToken);
        var eventId = await repository.StoreEventAsync(command.Event, imageUrl, cancellationToken);
        await PublishEventAsync(command.Event, cancellationToken);

        return new StoreEventResult(eventId);
    }

    private async Task<string> UploadImageAsync(IFormFile eventImage, CancellationToken cancellationToken)
    {
        // Create a memory stream to hold the image data
        using var memoryStream = new MemoryStream();
        
        // Copy the image data from the IFormFile to the memory stream asynchronously
        await eventImage.CopyToAsync(memoryStream, cancellationToken);
        
        // Get the byte array from the memory stream
        var imageData = memoryStream.ToArray();
        
        var imageUrl = await imageStorageClient.UploadImageAsync(new UploadImageRequest
        {
            Category = "Events",
            FileName = eventImage.FileName,
            ImageData = Google.Protobuf.ByteString.CopyFrom(imageData) // Convert the byte array to a Google Protobuf ByteString
        }, cancellationToken: cancellationToken);
        
        return imageUrl.ImageUrl;
    }

    private async Task PublishEventAsync(EventDto eventDto, CancellationToken cancellationToken)
    {
        var createEvent = new CreateEvent
        {
            Description = eventDto.Description,
            Name = eventDto.Name,
            StartDateTime = eventDto.StartDateTime,
            EndDateTime = eventDto.EndDateTime,
        };
        
        await publishEndpoint.Publish(createEvent, cancellationToken);
    }
}