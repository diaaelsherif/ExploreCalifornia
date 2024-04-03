using ExploreCalifornia;

var builder = WebApplication.CreateBuilder(args);

//add transient service - dependency injection
builder.Services.AddTransient<FeatureToggles>(x => new FeatureToggles
{
    //copy configuration variable to the property
    DeveloperExceptions = builder.Configuration.GetValue<bool>("FeatureToggles:DeveloperExceptions")
});

//add Mvc using legacy routing
builder.Services.AddMvc((setupAction) =>
{
    setupAction.EnableEndpointRouting = false;
});

var app = builder.Build();

//use injected dependency
var features = app.Services.GetRequiredService<FeatureToggles>();

app.UseExceptionHandler("/error.html");

if (features.DeveloperExceptions)
{
    app.UseDeveloperExceptionPage();
}

//Middleware
app.Use(async (context, next) =>
{
    //throw exception when request path value contains invalid
    if (context.Request.Path.Value.Contains("invalid"))
        throw new Exception("ERROR!");

    //move to the next middleware
    await next();
});

//use Mvc
app.UseMvc(routes =>
{
    routes.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
});

//use wwwroot folder contents
app.UseFileServer();

//run the webpage
app.Run();