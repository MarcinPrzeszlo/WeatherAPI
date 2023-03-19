using WeatherAPI.Exceptions;

namespace WeatherAPI.Middleware
{
    public class ErrorHandlingMiddleware: IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (BadRequestException badRequestException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(badRequestException.Message);
                _logger.LogError(badRequestException.Message);
            }
            catch (HttpRequestException httpException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(httpException.Message);
                _logger.LogError(httpException.Message);
            }
            catch (NullReferenceException nullException)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(nullException.Message);
                _logger.LogError(nullException.Message);
            }
            catch (ForbidException forbidException)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync(forbidException.Message);
                _logger.LogError(forbidException.Message);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
                _logger.LogError(ex.Message);
            }
        }
    }

}

