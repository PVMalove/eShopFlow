namespace Promotion.GRPS.Features.GetPromotions;

internal sealed class GetPromoByCatalogItemIdQueryHandler(IPromoRepository promoRepository)
    : IQueryHandler<GetPromoByCatalogItemIdQuery, PromotionResponse>
{
    public async Task<PromotionResponse> Handle(GetPromoByCatalogItemIdQuery query, CancellationToken cancellationToken)
    {
        var promo = await promoRepository.GetPromotionByCatalogItemIdAsync(query.CatalogItemId);

        if (promo is null)
        {
            throw new RpcException(
                new Status(StatusCode.NotFound, $"Для {query.CatalogItemId} не найдена акция"));
        }

        var result = promo.Adapt<PromotionResponse>();
        return result;
    }
}