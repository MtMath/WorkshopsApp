using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Workshops.Web.Middlewares;

/// <summary>
/// 
/// </summary>
public class AppExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "An error occurred",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            Detail = exception.Message,
            Instance = httpContext.Request.Path
        };
        
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        httpContext.Response.ContentType = "application/json";
        
        await httpContext.Response.WriteAsJsonAsync(details, cancellationToken);

        return true;
    }
}