using catalog.Domain.Entities;
using catalog.Domain.Specifications;

namespace catalog.Domain.Repositories;

public interface ICatalogItemRepository
{
    Task<CatalogItem> CreateCatalogItemAsync(CatalogItem item, CancellationToken cancellationToken = default);

    Task<IEnumerable<CatalogItem>> GetAllCatalogItemsAsync(CancellationToken cancellationToken = default);

    Task<CatalogItem?> GetCategoryItemByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<CatalogItem>> GetCatalogItemsByTitleAsync(string title,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<CatalogItem>> GetCatalogItemsByBrandAsync(string brandTitle,
        CancellationToken cancellationToken = default);

    Task<bool> UpdateCatalogItemAsync(CatalogItem item, CancellationToken cancellationToken = default);

    Task<bool> DeleteCatalogItemAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Pagination<CatalogItem>> GetCatalogItemsWithPaginationAsync(PaginationParameters parameters,
        CancellationToken cancellationToken = default);

    Task<Pagination<CatalogItem>> GetCatalogItemsWithFiltersAsync(
        FilterParameters parameters, CancellationToken cancellationToken = default);
}