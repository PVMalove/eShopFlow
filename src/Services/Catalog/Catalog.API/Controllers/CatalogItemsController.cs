using catalog.API.Controllers.Requests;
using catalog.Application.Commands.CatalogItems;

namespace catalog.API.Controllers;

public class CatalogItemsController : ApiController
{
    [HttpGet]
    [ProducesResponseType(typeof(GetCatalogItemsResult), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetCatalogItemsResult>> GetItems(CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new GetCatalogItemsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetCatalogItemByIdResult), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<GetCatalogItemByIdResult>> GetById(Guid id,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new GetCatalogItemByIdQuery(id), cancellationToken);
        if (result.Result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet("Titles/{title}")]
    [ProducesResponseType(typeof(GetCatalogItemsByTitleResult), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetCatalogItemsByTitleResult>> GetByTitle(string title,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new GetCatalogItemsByTitleQuery(title), cancellationToken);
        return Ok(result);
    }

    [HttpGet("Brands/{title}")]
    [ProducesResponseType(typeof(GetCatalogItemsByBrandTitleResult), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetCatalogItemsByBrandTitleResult>> GetByBrandTitle(string title,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new GetCatalogItemsByBrandTitleQuery(title), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateCatalogItemResult), (int)HttpStatusCode.Created)]
    public async Task<ActionResult<CreateCatalogItemResult>> Create(CreateCatalogItemCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);
        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Id },
            result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(UpdateCatalogItemResult), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<UpdateCatalogItemResult>> Update(Guid id, UpdateCatalogItemRequest request,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(request.ToCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(DeleteCatalogItemByIdResult), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<DeleteCatalogItemByIdResult>> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new DeleteCatalogItemByIdCommand(id), cancellationToken);
        return Ok(result);
    }
}