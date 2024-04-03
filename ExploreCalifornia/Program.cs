using ExploreCalifornia;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<FeatureToggles>(x => new FeatureToggles
{
    DeveloperExceptions = builder.Configuration.GetValue<bool>("FeatureToggles:DeveloperExceptions")
});

var app = builder.Build();
var features = app.Services.GetRequiredService<FeatureToggles>();

app.UseExceptionHandler("/error.html");

if (features.DeveloperExceptions)
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