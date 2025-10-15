namespace catalog.Application.Responses.CatalogItems;

public record GetCatalogItemsByTitleResult(IEnumerable<CatalogItem> CatalogItems);