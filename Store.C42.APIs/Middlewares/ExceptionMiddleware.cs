using Microsoft.AspNetCore.Http;
using Store.C42.APIs.Errors;
using System.Text.Json;

namespace Store.C42.APIs.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }


        public async Task InvokeAsync(HttpContext context)  //HttpContext : Represents the HTTP context of the request message and response message.
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var response = _env.IsDevelopment() ?
                    new ApiExceptionResponse(StatusCodes.Status500InternalServerError, ex.Message, ex.StackTrace.ToString())
                  : new ApiExceptionResponse(StatusCodes.Status500InternalServerError);   

                var json = JsonSerializer.Serialize(response);   //to convert the Response Message to JSON format, As the WriteAsync method accepts only string or byte array.

                context.Response.WriteAsync(json);
            }
        }
    }
}
