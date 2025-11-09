namespace Promotion.GRPS.Features.CreatePromotion;

public class CreatePromoCommandHandler(IPromoRepository promoRepository) 
    : ICommandHandler<CreatePromoCommand, CreatePromotionResponse>
{
    public async Task<CreatePromotionResponse> Handle(CreatePromoCommand command, CancellationToken cancellationToken)
    {
        var promo = command.Promotion.Adapt<Promo>();
        
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