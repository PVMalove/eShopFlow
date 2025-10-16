using catalog.Application.Responses.CatalogItems;

namespace catalog.Application.Queries.CatalogItems;

public record GetCatalogItemByIdQuery(Guid Id) : IQuery<GetCatalogItemByIdResult>;