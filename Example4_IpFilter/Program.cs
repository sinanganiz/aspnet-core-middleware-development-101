using System.Net;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseMiddleware<IpFilterMiddleware>();

app.MapGet("/", () => "Hello World!");
app.Run();

public class IpFilterMiddleware
{
    private readonly RequestDelegate _next;
    public IpFilterMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        var clientIp = context.Connection.RemoteIpAddress;

        // Uncomment below line to test access from a different IP
        // clientIp = IPAddress.Parse("203.0.113.5");

        bool isLocalhost = IPAddress.IsLoopback(clientIp);

        if (isLocalhost)
        {
            await _next(context);
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("This API is only accessible from localhost.");
        }
    }
}