using ExceptionFilter.Middleware.GlobalErrorHandling.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace ExceptionFilter.Middleware.GlobalErrorHandling.Handlers
{
    public class GlobalErrorHandler
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                // Call the next delegate/middleware in the pipeline.
                await _next(httpContext);
            }
            catch (Exception error)
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
}
