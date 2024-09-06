namespace EventBooking.Basket.Features.DeleteBasket;

public record DeleteBasketCommand() : ICommand<DeleteBasketResult>;

public record DeleteBasketResult(bool IsSuccess);

public class DeleteBasketCommandHandler
    (IBasketRepository repository)
    : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        var result = await repository.DeleteBasketAsync(cancellationToken);
        
        return new DeleteBasketResult(result);
    }
}