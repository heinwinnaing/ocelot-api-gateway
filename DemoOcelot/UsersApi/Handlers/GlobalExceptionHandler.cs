using Microsoft.AspNetCore.Diagnostics;
using UsersApi.Model;

namespace UsersApi.Handlers;

public class GlobalExceptionHandler
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = httpContext.Response?.StatusCode ?? 500;
        var error = ResultModel.Error(
            errorCode: httpContext.Response!.StatusCode,
            errorMessage: $"{exception.Message} {exception.InnerException?.Message}");

        await httpContext
            .Response
            .WriteAsJsonAsync(error);

        return true;
    }
}
