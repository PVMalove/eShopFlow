namespace catalog.API.Controllers;

public class BrandsController : ApiController
{
    [HttpGet]
    [ProducesResponseType(typeof(GetBrandsResult), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetBrandsResult>> GetBrand(CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new GetBrandsQuery(), cancellationToken);
        return Ok(result);
    }
}