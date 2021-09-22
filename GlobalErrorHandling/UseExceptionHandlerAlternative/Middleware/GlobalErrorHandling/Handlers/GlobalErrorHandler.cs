using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using UseExceptionHandlerAlternative.Middleware.GlobalErrorHandling.Exceptions;

namespace UseExceptionHandlerAlternative.Middleware.GlobalErrorHandling.Handlers
{
    public class GlobalErrorHandler
    {
        public static ActionResult<string> HandleException(Exception error)
        {
            JsonResult jsonResult = error switch
            {
                CustomException => new JsonResult(new { message = error?.Message })
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                },
                // Do not write the message from the error to the response body
                // as the message could contain sensitive information which is a security risk.
                // Write a basic error message to the client.
                _ => new JsonResult(new { message = "The server encountered an unhandled error." })
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                },
            };

            return jsonResult;

        }
    }
}
