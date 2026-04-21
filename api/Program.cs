using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // Registers the OpenAPI document generator
    app.MapScalarApiReference(options =>
    {
        options.Title = "Warehouse API reference";
        options.Theme = ScalarTheme.BluePlanet;
    }); // Serve the Scalar UI (default route is /scalar)
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/test", () =>
{
    string message = "Hello World";

    return message;
})
.WithName("GetWeatherForecast");

app.Run();

