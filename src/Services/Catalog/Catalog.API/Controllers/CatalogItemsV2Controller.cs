using Asp.Versioning;
using Swashbuckle.AspNetCore.Annotations;

namespace catalog.API.Controllers;

[ApiVersion("2.0")]
[ApiExplorerSettings(GroupName = "v2")]
[Route("api/v{version:apiVersion}/[controller]")]
[ControllerName("CatalogItems")] 
public class CatalogItemsV2Controller : ApiController
{
    [HttpGet]
    [SwaggerOperation(Tags = ["CatalogItems"])]
    [ProducesResponseType(typeof(GetCatalogItemsWithPaginationResult), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetCatalogItemsWithPaginationResult>> GetItems(
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 5,
        [FromQuery] string? sortBy = null,
        [FromQuery] bool sortDescending = false,
        CancellationToken cancellationToken = default)
    {
        var query = new GetCatalogItemsWithPaginationQuery(
            pageIndex,
            pageSize,
            sortBy,
            sortDescending);

        var result = await Sender.Send(query, cancellationToken);

        return Ok(result);
    }
    
    [HttpGet("search")]
    [SwaggerOperation(Tags = ["CatalogItems"])]
    [ProducesResponseType(typeof(GetCatalogItemsWithPaginationResult), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetCatalogItemsWithPaginationResult>> SearchItems(
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 5,
        [FromQuery] string? sortBy = null,
        [FromQuery] bool sortDescending = false,
        [FromQuery] string? searchTerm = null,
        [FromQuery] Guid? brandId = null,
        [FromQuery] Guid? categoryId = null,
        [FromQuery] decimal? minPrice = null,
        [FromQuery] decimal? maxPrice = null,
        CancellationToken cancellationToken = default)
    {
        var query = new GetCatalogItemsWithFiltersQuery(
            pageIndex,
            pageSize,
            sortBy,
            sortDescending,
            searchTerm,
            brandId,
            categoryId,
            minPrice,
            maxPrice);

        var result = await Sender.Send(query, cancellationToken);
        return Ok(result);
    }
}