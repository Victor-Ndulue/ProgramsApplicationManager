using Microsoft.AspNetCore.Diagnostics;
using NLog;

namespace WebApp.Middlewares;

public static class ExceptionMiddleware
{
    private static NLog.ILogger logger = LogManager.GetCurrentClassLogger();
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(
            appError => appError.Run(
                async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (exceptionFeature is not null)
                    {
                        var error = exceptionFeature.Error;
                        var endPoint = exceptionFeature.Endpoint;
                        logger.Error($"An error occurred processing request {endPoint}.", error);
                        await context.Response.WriteAsJsonAsync(new ErrorDetails
                        {
                            StatusCode = 500,
                            Message = $"An error encountered. Details: {error.Message}"
                        });
                    }
                })
            );
    }
}
public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
}
