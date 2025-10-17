namespace Basket.API.Features.ShoppingBasket.Save;

public class SaveCartCommandHandler(ICartRepository cartRepository) : ICommandHandler<SaveCartCommand, SaveCartResult>
{
    public async Task<SaveCartResult> Handle(SaveCartCommand command, CancellationToken cancellationToken)
    {
        var cart = command.Card;
        await cartRepository.SaveCartAsync(cart, cancellationToken);
        return new SaveCartResult(cart.AccountName);
    }
}