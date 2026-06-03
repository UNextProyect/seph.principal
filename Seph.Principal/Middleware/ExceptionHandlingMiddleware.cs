using Seph.Principal.Application.Common.Models;
using System.Net;
using System.Text.Json;
using FluentValidation;

namespace Seph.Principal.Middleware
{
    public sealed class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (ValidationException exception)
            {
                await WriteResponseAsync(context, HttpStatusCode.BadRequest, "Error de validación", exception.Errors.Select(error => error.ErrorMessage));
            }
            catch (UnauthorizedAccessException exception)
            {
                await WriteResponseAsync(context, HttpStatusCode.Unauthorized, exception.Message, Array.Empty<string>());
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Unhandled exception");
                await WriteResponseAsync(context, HttpStatusCode.InternalServerError, "Ocurrió un error inesperado", Array.Empty<string>());
            }
        }

        private static async Task WriteResponseAsync<T>(HttpContext context, HttpStatusCode statusCode, string message, T data)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new ResponseWrapper<T>
            {
                StatusCode = statusCode,
                Message = message,
                Data = data
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response, new JsonSerializerOptions(JsonSerializerDefaults.Web)));
        }
    }

}
