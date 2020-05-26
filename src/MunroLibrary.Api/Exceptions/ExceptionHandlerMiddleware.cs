using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MunroLibrary.Api.Exceptions
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            this.next = next;
            logger = loggerFactory.CreateLogger("Global exception logger");
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception exception)
            {
                logger.LogError(
                    exception,
                    exception.Message);

                if (httpContext.Response.HasStarted)
                {
                    logger.LogWarning("The response has already started, the ExceptionHandler middleware will not be executed.");
                    throw;
                }

                await ExceptionHandler.HandleExceptionAsync(httpContext, exception);
            }
        }
    }
}
