using Basket.API.Services;

namespace Basket.API.Features.ShoppingBasket.Save;

internal sealed class SaveCartCommandHandler(
    ICartRepository cartRepository,
    IBasketService basketService,
    ILogger<SaveCartCommandHandler> logger)
    : ICommandHandler<SaveCartCommand, SaveCartResult>
{
    public async Task<SaveCartResult> Handle(SaveCartCommand command, CancellationToken cancellationToken)
    {
        var cart = command.Cart;
        var shoppingCart = await basketService.CalculateDiscountsAsync(cart, cancellationToken);
        await cartRepository.SaveCartAsync(shoppingCart, cancellationToken);
        return new SaveCartResult(shoppingCart.AccountName);
    }
}