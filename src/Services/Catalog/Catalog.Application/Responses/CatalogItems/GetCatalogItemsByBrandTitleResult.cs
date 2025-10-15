namespace catalog.Application.Responses.CatalogItems;

public record GetCatalogItemsByBrandTitleResult(IEnumerable<CatalogItem> CatalogItems);