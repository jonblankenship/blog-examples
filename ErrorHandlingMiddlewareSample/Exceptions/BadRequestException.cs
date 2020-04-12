using System;

namespace ErrorHandlingMiddlewareSample.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        { }
    }
}
