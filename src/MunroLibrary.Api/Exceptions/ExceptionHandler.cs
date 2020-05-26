using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MunroLibrary.Api.Exceptions
{
    public class ExceptionHandler
    {
        private static string GetJsonResponse(string message)
        {
            return JsonConvert.SerializeObject(
                    new ApiErrorResponse
                    {
                        Message = message,
                    },
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    });
        }

        private static (HttpStatusCode httpStatusCode, string message) GetExceptionResult(Exception exception)
        {
            // Unknown errors (we don't want to expose any internal messages for security reasons)
            var httpStatusCode = HttpStatusCode.InternalServerError;
            var message = "An unexpected error occurred, please try again later!";

            // Custom Api Errors
            if (exception is ApiException apiException)
            {
                message = apiException.Message;
                httpStatusCode = apiException.HttpStatusCode;
            }

            // Bad Request errors
            if (exception is ArgumentException)
            {
                httpStatusCode = HttpStatusCode.BadRequest;
                message = exception.Message;
            }

            return (httpStatusCode, message);
        }

        public static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorResult = GetExceptionResult(exception);

            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)errorResult.httpStatusCode;
            await response.WriteAsync(GetJsonResponse(errorResult.message));
        }
    }
}