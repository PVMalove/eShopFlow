namespace Promotion.GRPS.Features.GetPromotions;

internal sealed class GetPromoByCatalogItemIdQueryHandler(IPromoRepository promoRepository)
    : IQueryHandler<GetPromoByCatalogItemIdQuery, PromotionResponse>
{
    public async Task<PromotionResponse> Handle(GetPromoByCatalogItemIdQuery query, CancellationToken cancellationToken)
    {
        var promo = await promoRepository.GetPromotionByCatalogItemIdAsync(query.CatalogItemId);

        if (promo is null)
        {
            return new PromotionResponse { Value = 0, CatalogItemId = query.CatalogItemId };
        }
        
        var result = promo.Adapt<PromotionResponse>();
        return result;
    }
}