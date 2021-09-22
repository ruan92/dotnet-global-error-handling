using System;

namespace ExceptionFilter.Middleware.GlobalErrorHandling.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException() : base() { }
        public CustomException(string message) : base(message) { }
    }
}
