namespace Basket.API.Features.ShoppingBasket.Remove;

internal sealed class RemoveCartCommandHandler(ICartRepository cartRepository) : ICommandHandler<RemoveCartCommand, RemoveCartResult>
{
    public async Task<RemoveCartResult> Handle(RemoveCartCommand command, CancellationToken cancellationToken)
    {
        var result = await cartRepository.RemoveCartAsync(command.AccountName, cancellationToken);
        return new RemoveCartResult(result);
    }
}