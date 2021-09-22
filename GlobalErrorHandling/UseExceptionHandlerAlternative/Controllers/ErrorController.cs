using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UseExceptionHandlerAlternative.Middleware.GlobalErrorHandling.Handlers;

namespace UseExceptionHandlerAlternative.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        public ActionResult<string> Error()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionHandlerPathFeature?.Error is not null)
            {
                return GlobalErrorHandler.HandleException(exceptionHandlerPathFeature.Error);
            }

            return new JsonResult(new { message = "The server encountered an unhandled error." })
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }
    }
}
