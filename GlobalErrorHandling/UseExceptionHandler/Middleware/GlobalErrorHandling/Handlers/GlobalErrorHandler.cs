using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using UseExceptionHandler.Middleware.GlobalErrorHandling.Exceptions;

namespace UseExceptionHandler.Middleware.GlobalErrorHandling.Handlers
{
    public class GlobalErrorHandler
    {
        public static async Task HandleException(Exception error, HttpContext httpContext)
        {
            var response = httpContext.Response;

            response.ContentType = "application/json";
            string message;

            switch (error)
            {
                case CustomException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    message = error?.Message;
                    break;

                // Do not write the message from the error to the response body
                // as the message could contain sensitive information which is a security risk.
                // Write a basic error message to the client.
                default:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    message = "The server encountered an unhandled error.";
                    break;
            }

            var jsonMessage = JsonSerializer.Serialize(new { message });
            await response.WriteAsync(jsonMessage);
        }
    }
}
