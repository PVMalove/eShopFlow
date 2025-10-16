namespace Basket.API.Infrastructure;

public interface ICartRepository
{
    Task<ShoppingCart> GetCartAsync(string accountName, CancellationToken cancellationToken = default);

    Task<ShoppingCart> SaveCartAsync(ShoppingCart cart, CancellationToken cancellationToken = default);

    Task<bool> RemoveCartAsync(string accountName, CancellationToken cancellationToken = default);
}