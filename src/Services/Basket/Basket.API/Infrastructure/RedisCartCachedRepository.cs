using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;


namespace Basket.API.Infrastructure;

public class RedisCartCachedRepository(ICartRepository cartRepository, IDistributedCache cache) : ICartRepository
{
    public async Task<ShoppingCart> GetCartAsync(string accountName, CancellationToken cancellationToken = default)
    {
        var cartCached = await cache.GetStringAsync(accountName, cancellationToken);
        if (!string.IsNullOrEmpty(cartCached))
        {
            return JsonSerializer.Deserialize<ShoppingCart>(cartCached)!;
        }
        
        var cart = await cartRepository.GetCartAsync(accountName, cancellationToken);
        {
            var serializedCart = JsonSerializer.Serialize(cart);
            await cache.SetStringAsync(accountName, serializedCart, cancellationToken);
        }
        return cart;
    }

    public async Task<ShoppingCart> SaveCartAsync(ShoppingCart cart, CancellationToken cancellationToken = default)
    {
        var result = await cartRepository.SaveCartAsync(cart, cancellationToken);
        var serializedCart = JsonSerializer.Serialize(cart);
        await cache.SetStringAsync(cart.AccountName, serializedCart, cancellationToken);
        return result;
    }

    public async Task<bool> RemoveCartAsync(string accountName, CancellationToken cancellationToken = default)
    {
        var result = await cartRepository.RemoveCartAsync(accountName, cancellationToken);
        await cache.RemoveAsync(accountName, cancellationToken);
        return result;
    }
}