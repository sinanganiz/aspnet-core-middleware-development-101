var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Request Logging Middleware
app.Use(async (context, next) =>
{
    Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
    await next(); // Next middleware
});

app.MapGet("/", () => "Hello World!");

app.Run();