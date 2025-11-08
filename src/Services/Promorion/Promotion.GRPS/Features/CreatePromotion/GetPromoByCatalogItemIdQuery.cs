namespace Promotion.GRPS.Features.CreatePromotion;

public record CreatePromoCommand(CreatePromotionRequest promotion) : ICommand<CreatePromotionResponse>;