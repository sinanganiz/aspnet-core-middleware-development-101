var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// First Middleware
app.Use(async (context, next) =>
{
    Console.WriteLine("➡️  Entering First Middleware");

    await next();

    Console.WriteLine("⬅️  Exiting First Middleware");
});

// Second Middleware
app.Use(async (context, next) =>
{
    Console.WriteLine("➡️  Entering Second Middleware");

    await next();

    Console.WriteLine("⬅️  Exiting Second Middleware");
});

app.MapGet("/", () => "Hello World!");
app.Run();
