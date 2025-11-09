namespace Promotion.GRPS.MappingConfiguration;

public static class Mapping
{
    public static void Configure()
    {
        TypeAdapterConfig<CreatePromotionRequest, Promo>
            .NewConfig()
            .Map(dest => dest.Id, src => Guid.NewGuid())
            .Map(dest => dest.Value, src => (decimal)src.Value);
        
        TypeAdapterConfig<UpdatePromotionRequest, Promo>
            .NewConfig()
            .Map(dest => dest.Id, src => Guid.Parse(src.Id))
            .Map(dest => dest.Value, src => (decimal)src.Value);
    }
}