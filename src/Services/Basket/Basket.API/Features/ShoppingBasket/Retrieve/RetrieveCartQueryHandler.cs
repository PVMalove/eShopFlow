namespace Basket.API.Features.ShoppingBasket.Retrieve;

internal sealed class RetrieveCartQueryHandler(ICartRepository cartRepository)
    : IQueryHandler<RetrieveCartQuery, RetrieveCartResult>
{
    public async Task<RetrieveCartResult> Handle(RetrieveCartQuery request, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetCartAsync(request.AccountName, cancellationToken);
        return new RetrieveCartResult(cart);
    }
}