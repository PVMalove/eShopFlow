namespace Promotion.GRPS.Features.DeletePromotion;

public class DeletePromoCommandHandler(IPromoRepository promoRepository) 
    : ICommandHandler<DeletePromoCommand, DeletePromotionResponse>
{
    public async Task<DeletePromotionResponse> Handle(DeletePromoCommand command, CancellationToken cancellationToken)
    {
        var success =  await promoRepository.DeletePromotionByCatalogItemIdAsync(command.CatalogItemId);
        
        if (!success)
        {
            throw new RpcException(
                new Status(StatusCode.Internal, "Не удалось удалить промо-акцию"));
        }
        
        var result = new DeletePromotionResponse
        {
            Success = success
        };

        return result;
    }
}