using System;
using System.Net;

namespace MunroLibrary.Api.Exceptions
{
    public class ApiException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; }

        public ApiException(string message, HttpStatusCode httpStatusCode)
            : base(message)
        {
            HttpStatusCode = httpStatusCode;
        }
    }
}
