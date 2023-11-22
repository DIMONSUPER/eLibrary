using BGNet.TestAssignment.Common.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace BGNet.TestAssignment.Common;

public static class DependencyInjection
{
    public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlerMiddleware>();

        return app;
    }
}
