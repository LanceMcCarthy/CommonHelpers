using CommonHelpers.Middleware.Extensions;
using CommonHelpers.Middleware.Services;
using CommonHelpers.Middleware.Interfaces;
using CommonHelpers.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using CommonHelpers.Middleware.Services.Models;

// Normal builder
var builder = WebApplication.CreateBuilder(args);

// Key point //
// Registering one of the scoped services available in the middleware
builder.Services.AddScoped<IMessageWriter, LoggingMessageWriter>();

// Registering a singleton
builder.Services.TryAddSingleton<SampleDataService>();


builder.Services.AddCommonHelpersServices(options =>
        options.ComicVineApiKey = ""
    );


// Normal app stuff
var app = builder.Build();
app.UseHttpsRedirection();


// Key Point //
// Registering the middleware
app.UseMyCustomMiddleware();



// simple homepage for the test
app.MapGet("/", () => "Hello World!");

app.Run();