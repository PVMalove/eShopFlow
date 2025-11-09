namespace Promotion.GRPS.Features.UpdatePromotion;

public record UpdatePromoCommand(UpdatePromotionRequest Promotion) : ICommand<UpdatePromotionResponse>;

public class UpdatePromoCommandHandler(IPromoRepository promoRepository) 
    : ICommandHandler<UpdatePromoCommand, UpdatePromotionResponse>
{
    public async Task<UpdatePromotionResponse> Handle(UpdatePromoCommand command, CancellationToken cancellationToken)
    {
        var promo = command.Promotion.Adapt<Promo>();
        
        var success = await promoRepository.UpdatePromotionAsync(promo);
        
        if (!success)
        {
            throw new RpcException(
                new Status(StatusCode.Internal, "Не удалось обновить промо-акцию"));
        }
        
        var result = new UpdatePromotionResponse
        {
            Success = success
        };

        return result;
    }
}