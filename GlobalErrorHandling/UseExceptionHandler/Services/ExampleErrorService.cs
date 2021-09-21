using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UseExceptionHandler.Middleware.GlobalErrorHandling.Exceptions;

namespace UseExceptionHandler.Services
{
    public class ExampleErrorService : IExampleErrorService
    {
        public void ThrowCustomException()
        {
            throw new CustomException("This is an example of a custom exception.");
        }

        public void ThrowNullReferenceException()
        {
            // The message of this NullReferenceException will not be sent to the client as the GlobalErrorHandler will not use this message for security reasons.
            throw new NullReferenceException("This is a null reference exception which is an example of a typical exception that might be thrown by the server.");
        }
    }

    public interface IExampleErrorService
    {
        public void ThrowCustomException();
        public void ThrowNullReferenceException();
    }
}
