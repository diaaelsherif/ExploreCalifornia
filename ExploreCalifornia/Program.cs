var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var configuration = app.Configuration;

app.UseExceptionHandler("/error.html");

if (configuration.GetValue<bool>("EnableDeveloperExceptions"))
{
    app.UseDeveloperExceptionPage();
}

app.Use(async (context, next) =>
{
    if (context.Request.Path.Value.Contains("invalid"))
        throw new Exception("ERROR!");
    
    await next();
});

app.UseFileServer();
app.Run();