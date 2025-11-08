namespace Promotion.GRPS.Features.CreatePromotion;

public class CreatePromoCommandHandler(IPromoRepository promoRepository) 
    : ICommandHandler<CreatePromoCommand, CreatePromotionResponse>
{
    public async Task<CreatePromotionResponse> Handle(CreatePromoCommand command, CancellationToken cancellationToken)
    {
        var promo = new Promo
        {
            Id = Guid.NewGuid(),
            CatalogItemId = command.promotion.CatalogItemId,
            Title = command.promotion.Title,
            Value = (decimal)command.promotion.Value
        };
        
        var createdPromo = await promoRepository.CreatePromotionAsync(promo);
        
        if (!createdPromo)
        {
            throw new RpcException(
                new Status(StatusCode.Internal, "Не удалось создать промо-акцию"));
        }
        
        var result = new CreatePromotionResponse
        {
            Id = promo.Id.ToString(),
        };
        
        return result;
    }
}