public class NotFoundHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public NotFoundHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        await _next(context);

        if (context.Response.StatusCode == 404 && !context.Response.HasStarted)
        {
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync($"{{\"error\": \"unknow URL Path. current PATH：{context.Request.Path}\"}}");
        }
    }
}