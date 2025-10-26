using Basket.API.Exceptions;

namespace Basket.API.Infrastructure;

public class CartRepository(IDocumentSession session) : ICartRepository
{
    public async Task<ShoppingCart> GetCartAsync(string accountName, CancellationToken cancellationToken = default)
    {
        var cart = await session.LoadAsync<ShoppingCart>(accountName, cancellationToken);

        if (cart is null)
        {
            throw new CartNotFoundException(accountName);
        }

        return cart;
    }

    public async Task<ShoppingCart> SaveCartAsync(ShoppingCart cart, CancellationToken cancellationToken = default)
    {
        session.Store(cart);
        await session.SaveChangesAsync(cancellationToken);
        return cart;
    }

    public async Task<bool> RemoveCartAsync(string accountName, CancellationToken cancellationToken = default)
    {
        var cart = await session.LoadAsync<ShoppingCart>(accountName, cancellationToken);

        if (cart is null)
        {
            throw new CartNotFoundException(accountName);
        }

        session.Delete(cart);
        await session.SaveChangesAsync(cancellationToken);
        return true;
    }
}