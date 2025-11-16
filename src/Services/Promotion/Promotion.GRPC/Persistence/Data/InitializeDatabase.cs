namespace Promotion.GRPS.Persistence.Data;

public static class InitializeDatabase
{
    private const string DropTableSql = "DROP TABLE IF EXISTS Promos;";
    private const string CreateTableSql = "CREATE TABLE IF NOT EXISTS Promos (" +
                                          "Id CHAR(36) NOT NULL PRIMARY KEY," +
                                          "CatalogItemId VARCHAR(255) NOT NULL," +
                                          "Title VARCHAR(255) NOT NULL," +
                                          "Value DECIMAL(18,2) NOT NULL" +
                                          ");";
    
    public static async Task SeedAsync(IDbConnection connection)
    {
        if(connection.State != ConnectionState.Open)
            connection.Open();

        await connection.ExecuteAsync(DropTableSql);
        await connection.ExecuteAsync(CreateTableSql);
        
        
        foreach (var promo in InitializeData.GetInitialPromos)
        {
            var insertSql = "INSERT INTO Promos (Id, CatalogItemId, Title, Value) VALUES (@Id, @CatalogItemId, @Title, @Value);";
            await connection.ExecuteAsync(insertSql, new
            {
                promo.Id,
                promo.CatalogItemId,
                promo.Title,
                promo.Value
            });
        }
    }
}