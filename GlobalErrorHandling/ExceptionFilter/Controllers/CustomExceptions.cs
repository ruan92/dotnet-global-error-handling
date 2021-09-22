using ExceptionFilter.Middleware.GlobalErrorHandling.Handlers;
using ExceptionFilter.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExceptionFilter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(CustomExceptionFilter))]
    public class CustomExceptions : ControllerBase
    {
        private readonly IExampleErrorService _exampleErrorService;

        public CustomExceptions(IExampleErrorService exampleErrorService)
        {
            _exampleErrorService = exampleErrorService;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            _exampleErrorService.ThrowCustomException();

            return "This will not be returned to the client due to the exception that will be thrown.";
        }
    }
}
