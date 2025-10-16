using catalog.Application.Responses.CatalogItems;

namespace catalog.Application.Commands.CatalogItems;

public record DeleteCatalogItemByIdCommand(Guid Id) : ICommand<DeleteCatalogItemByIdResult>;