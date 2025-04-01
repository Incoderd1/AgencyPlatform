using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
using System;

namespace AgencyPlatform.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);

                // Captura errores de validación del modelo que no lanzan excepciones
                if (context.Response.StatusCode == (int)HttpStatusCode.BadRequest && context.Items["__ModelStateErrors"] is ModelStateDictionary modelState)
                {
                    var errors = modelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    var result = JsonSerializer.Serialize(new
                    {
                        error = true,
                        message = "Error de validación de datos.",
                        details = errors
                    });

                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(result);
                }
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var result = JsonSerializer.Serialize(new
                {
                    error = true,
                    message = ex.Message
                });

                await context.Response.WriteAsync(result);
            }
        }
    }

    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}