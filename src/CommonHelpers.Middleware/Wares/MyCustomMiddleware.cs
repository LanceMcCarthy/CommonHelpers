using CommonHelpers.Middleware.Interfaces;

namespace CommonHelpers.Middleware.Wares;

public class MyCustomMiddleware
{
    private readonly RequestDelegate next;

    public MyCustomMiddleware(RequestDelegate next)
    {
        this.next = next;
    }
    
    public async Task InvokeAsync(HttpContext httpContext, IMessageWriter service)
    {
        // do the job of whatever this service does
        service.Write($"You invoked the middleware service at {DateTime.Now.Ticks.ToString()}");

        // invoke the next item in the middleware pipeline
        await next(httpContext);
    }
}