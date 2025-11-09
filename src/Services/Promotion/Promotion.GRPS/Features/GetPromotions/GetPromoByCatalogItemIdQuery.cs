namespace Promotion.GRPS.Features.GetPromotions;

public record GetPromoByCatalogItemIdQuery(string CatalogItemId) : IQuery<PromotionResponse>;