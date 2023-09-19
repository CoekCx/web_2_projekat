using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NewAvalon.Abstractions.Exceptions;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace NewAvalon.App.Middlewares
{
    public sealed class ExceptionHandlerMiddleware : IMiddleware
    {
        private static readonly JsonSerializerOptions JsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger) => _logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ApiException apiException)
            {
                _logger.LogError(apiException, apiException.Message);

                await HandleExceptionAsync(context, apiException);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, ApiException apiException)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = apiException.Status;

            var response = new
            {
                apiException.Title,
                apiException.Type,
                apiException.Status,
                apiException.Detail,
                Errors = apiException.Errors.SelectMany(
                    keyValuePair => keyValuePair.Value,
                    (keyValuePair, errorMessage) => new
                    {
                        PropertyName = keyValuePair.Key,
                        ErrorMessage = errorMessage
                    })
            };

            string responseContent = JsonSerializer.Serialize(response, JsonSerializerOptions);

            await context.Response.WriteAsync(responseContent);
        }
    }
}
