namespace Promotion.GRPS.Persistence.Repositories;

internal sealed class PromoRepository(IDbConnection connection) : IPromoRepository
{
    public async Task<Promo?> GetPromotionByCatalogItemIdAsync(string catalogItemId)
    {
        const string sql =
            $"""
             SELECT
                 Id AS {nameof(Promo.Id)},
                 CatalogItemId AS {nameof(Promo.CatalogItemId)},
                 Title AS {nameof(Promo.Title)},
                 Value AS {nameof(Promo.Value)}
             FROM Promos
             WHERE CatalogItemId = @catalogItemId
             """;
        var result = await connection.QueryFirstOrDefaultAsync<Promo>(sql, new {catalogItemId});
        return result;
    }
    
    public async Task<bool> CreatePromotionAsync(Promo promo)
    {
        const string sql =
            """
            INSERT INTO Promos (Id, CatalogItemId, Title, Value)
            VALUES (@Id, @CatalogItemId, @Title, @Value)
            """;
        var affectedRows = await connection.ExecuteAsync(sql, promo);
        return affectedRows > 0;
    }

    public async Task<bool> UpdatePromotionAsync(Promo promo)
    {
        const string sql =
            """
            UPDATE Promos
            SET Title = @Title,
                Value = @Value
            WHERE Id = @Id
            """;
        var affectedRows = await connection.ExecuteAsync(sql, promo);
        return affectedRows > 0;
    }
}