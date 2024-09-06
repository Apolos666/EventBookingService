namespace EventBooking.Basket.Features.GetBasket;

public record GetBasketQuery() : IQuery<GetBasketResult>;

public record GetBasketResult(EventCart Cart);

public class GetBasketQueryHandler
    (IBasketRepository repository)
    : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        var result = await repository.GetBasketAsync(cancellationToken);
        
        return new GetBasketResult(result);
    }
}