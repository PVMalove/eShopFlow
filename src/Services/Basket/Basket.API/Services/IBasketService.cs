namespace Basket.API.Services;

public interface IBasketService
{
    Task<ShoppingCart> CalculateDiscountsAsync(ShoppingCart cart, CancellationToken cancellationToken);
}