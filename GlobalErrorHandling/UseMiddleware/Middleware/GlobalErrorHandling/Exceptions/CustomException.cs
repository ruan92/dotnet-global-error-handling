using System;

namespace UseMiddleware.Middleware.GlobalErrorHandling.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException() : base() { }
        public CustomException(string message) : base(message) { }
    }
}
