using Microsoft.AspNetCore.Http;
using System.Net;
using Serilog;

namespace OrdersApi.Shared.Middleware
{
    
    public class ApiExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ApiExceptionMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unhandled exception occurred");
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsJsonAsync(new { error = ex.Message });
            }
        }
    }
}
