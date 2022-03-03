using Microsoft.AspNetCore.Mvc;
using UseExceptionHandler.Services;

namespace UseExceptionHandler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServerExceptions : ControllerBase
    {
        private readonly IExampleErrorService _exampleErrorService;

        public ServerExceptions(IExampleErrorService exampleErrorService)
        {
            _exampleErrorService = exampleErrorService;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            _exampleErrorService.ThrowNullReferenceException();

            return "This will not be returned to the client due to the exception that will be thrown.";
        }
    }
}
