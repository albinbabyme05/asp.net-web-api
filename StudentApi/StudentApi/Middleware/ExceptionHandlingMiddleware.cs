using System.Net;
using System.Text.Json;

namespace StudentApi.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _iLogger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> iLogger)
        {
            _next = next;
            _iLogger = iLogger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                _iLogger.LogError(ex, "An unexpected error occured.");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var res = new
                {
                    message = "Something went wrong.Please try again later."
                };

                var jsonRes = JsonSerializer.Serialize(res);

                await context.Response.WriteAsync(jsonRes);
            }
        }
    }
}
