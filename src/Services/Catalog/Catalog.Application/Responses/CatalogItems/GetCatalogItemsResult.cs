namespace catalog.Application.Responses.CatalogItems;

public record GetCatalogItemsResult(IEnumerable<CatalogItem> CatalogItems);