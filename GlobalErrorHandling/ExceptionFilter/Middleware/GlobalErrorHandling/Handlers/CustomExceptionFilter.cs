using ExceptionFilter.Middleware.GlobalErrorHandling.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ExceptionFilter.Middleware.GlobalErrorHandling.Handlers
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public CustomExceptionFilter(
            IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public void OnException(ExceptionContext context)
        {
            if (!_hostingEnvironment.IsDevelopment())
            {
                return;
            }

            var error = context.Exception;
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
            context.Result = jsonResult;
        }
    }
}
