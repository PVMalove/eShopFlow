namespace catalog.API.Controllers;

public class CategoriesController : ApiController
{
    [HttpGet]
    [ProducesResponseType(typeof(GetCategoriesResult), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetCategoriesResult>> GetCategories(CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new GetCategoriesQuery(), cancellationToken);
        return Ok(result);
    }
}