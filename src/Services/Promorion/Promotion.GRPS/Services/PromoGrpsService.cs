using Promotion.GRPS.Features.CreatePromotion;

namespace Promotion.GRPS.Services;

internal sealed class PromoGrpsService(ISender sender) : PromotionService.PromotionServiceBase
{
    public override async Task<PromotionResponse> GetPromotion(GetPromotionRequest request, ServerCallContext context)
    {
        var query = new GetPromoByCatalogItemIdQuery(request.CatalogItemId); 
        var result = await sender.Send(query, context.CancellationToken);
        return result;
    }

    public override async Task<CreatePromotionResponse> CreatePromotion(CreatePromotionRequest request, ServerCallContext context)
    {
        var command = new CreatePromoCommand(request);
        var result = await sender.Send(command, context.CancellationToken);
        return result;
    }
}