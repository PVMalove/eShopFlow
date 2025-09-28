using Catalog.Domain.Entities;

namespace catalog.Domain.Repositories;

public interface ICatalogItemRepository
{
    Task<CatalogItem> CreateCatalogItemAsync(CatalogItem item);
    Task<IEnumerable<CatalogItem>> GetAttCatalogItemsAsync();
    Task<CatalogItem> GetCatalogItemAsync(Guid id);
    Task<IEnumerable<CatalogItem>> GetCatalogItemsByTitleAsync(string title);
    Task<IEnumerable<CatalogItem>> GetCatalogItemsByBrandAsync(string brandTitle);
    Task<bool> updateCatalogItemAsync(CatalogItem item);
    Task<bool> DeleteCatalogItemAsync(Guid id);
}
