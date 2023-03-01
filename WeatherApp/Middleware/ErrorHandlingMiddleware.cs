using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using WeatherApp.Exceptions;

namespace WeatherApp.Middleware
{
    public class ErrorHandlingMiddleware: IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (ConnectionFailedException connectionFailedException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(connectionFailedException.Message);
                
            }
            catch (NotFoundException notFoundException)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundException.Message);
            }
        }
    }
}
