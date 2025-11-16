using Grpc.Core;
using Promotion.GRPS.Protos;

namespace Basket.API.Services;

public class BasketService(
    PromotionService.PromotionServiceClient promotionService,
    ILogger<BasketService> logger)
    : IBasketService
{
    public async Task<ShoppingCart> CalculateDiscountsAsync(ShoppingCart cart, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(cart);

        var promotionTasks = cart.Items
            .Select(item => ApplyPromotionToItemAsync(item, cancellationToken))
            .ToArray();

        await Task.WhenAll(promotionTasks);
        return cart;
    }

    private async Task ApplyPromotionToItemAsync(ShoppingCartItem item, CancellationToken cancellationToken)
    {
        try
        {
            var request = new GetPromotionRequest { CatalogItemId = item.ItemId.ToString() };
            var promotion = await promotionService.GetPromotionAsync(request, cancellationToken: cancellationToken);

            if (promotion?.Value > 0)
            {
                item.UnitPrice = Math.Max(0, item.UnitPrice - (decimal)promotion.Value);
            }
        }
        catch (RpcException ex)
        {
            logger.LogWarning(ex, "Failed to get promotion for item {ItemId}. Status: {StatusCode}",
                item.ItemId, ex.StatusCode);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error applying promotion to item {ItemId}", item.ItemId);
        }
    }
}