namespace Promotion.GRPS.Persistence.Abstractions;

public interface IPromoRepository
{
    Task<Promo?> GetPromotionByCatalogItemIdAsync(string catalogItemId);
    Task<bool> CreatePromotionAsync(Promo promo);
    Task<bool> UpdatePromotionAsync(Promo promo);
}