namespace EventBooking.Basket.Features.DeleteBasket;

public record DeleteBasketCommand(Guid UserId) : ICommand<DeleteBasketResult>;

public record DeleteBasketResult(bool IsSuccess);

public class DeleteBasketCommandHandler
    (IBasketRepository repository)
    : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        var result = await repository.DeleteBasketAsync(command.UserId, cancellationToken);
        
        return new DeleteBasketResult(result);
    }
}