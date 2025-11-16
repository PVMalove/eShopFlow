namespace Promotion.GRPS.Features.DeletePromotion;

public record DeletePromoCommand(string CatalogItemId) : ICommand<DeletePromotionResponse>;