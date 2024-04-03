var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Middleware
app.Use(async (context, next) =>
{
    if (context.Request.Path.Value.StartsWith("/hello"))
    {
        await context.Response.WriteAsync("Hello ASP.NET Core!");
    }

    await next();
});

//Terminal Middleware
app.Run(async (context) =>
{
    await context.Response.WriteAsync(" How are you?");
});

app.Run();