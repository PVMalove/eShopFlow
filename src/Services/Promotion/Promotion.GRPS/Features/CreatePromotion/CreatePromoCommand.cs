namespace Promotion.GRPS.Features.CreatePromotion;

public record CreatePromoCommand(CreatePromotionRequest Promotion) : ICommand<CreatePromotionResponse>;