namespace UserService.Middleware;

public class NotFoundHandlerMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        await next(context);

        if (context.Response.StatusCode == 404 && !context.Response.HasStarted)
        {
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(
                $"{{\"error\": \"unknow URL Path. current PATH：{context.Request.Path}\"}}");
        }
    }
}