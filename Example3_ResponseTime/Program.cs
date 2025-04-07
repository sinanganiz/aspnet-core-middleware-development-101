var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseMiddleware<ResponseTimeMiddleware>();

app.MapGet("/", () => "Hello World!");
app.Run();

public class ResponseTimeMiddleware
{
    private readonly RequestDelegate _next;
    public ResponseTimeMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        var method = context.Request.Method;
        var path = context.Request.Path;

        await _next(context);

        stopwatch.Stop();
        var elapsedMs = stopwatch.ElapsedMilliseconds;

        Console.WriteLine($"[{DateTime.Now}] {method} {path} - Response time: {elapsedMs}ms (HTTP {context.Response.StatusCode})");
    }
}