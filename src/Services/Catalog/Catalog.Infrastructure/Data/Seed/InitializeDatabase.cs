using Marten.Schema;

namespace catalog.Infrastructure.Data.Seed;

public class InitializeDatabase : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        await using var session = store.LightweightSession();

        if (!await session.Query<Brand>().AnyAsync(cancellation))
        {
            session.Store<Brand>(InitialData.Brands);
        }

        foreach (var category in InitialData.Categories)
        {
            if (!await session.Query<Category>().AnyAsync(c => c.Id == category.Id, cancellation))
            {
                session.Store(category);
            }
        }

        foreach (var catalogItem in InitialData.CatalogItems)
        {
            if (!await session.Query<CatalogItem>().AnyAsync(ci => ci.Id == catalogItem.Id, cancellation))
            {
                session.Store(catalogItem);
            }
        }

        await session.SaveChangesAsync(cancellation);
    }
}
