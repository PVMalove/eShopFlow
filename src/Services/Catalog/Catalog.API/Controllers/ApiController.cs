namespace catalog.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private ISender? sender;

        protected ISender Sender =>
            sender ??= HttpContext.RequestServices.GetService<ISender>()
                       ?? throw new InvalidOperationException("Служба ISenser недоступна");
    }
}