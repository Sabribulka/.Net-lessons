using Microsoft.AspNetCore.Http.HttpResults;
using WebApplication1.Models;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<IWeatherService, WeatherService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/weatherforecast", Results<Ok<WeatherForecast[]>, BadRequest<string>> (IWeatherService weatherService, int? days) =>
    {
        if (days < 1 || days > 30)
        {
            return TypedResults.BadRequest("Invalid number");
        }
        
        return TypedResults.Ok(weatherService.GetData(days));
    })
    .WithName("GetWeatherforecast");

app.MapGet("/hello", () =>
    {
        return "Hello Anna!";
    })
    .WithName("GetHello");

app.MapGet("/hello/{name}", (string name) =>
        $"Hello {name}!")
    .WithName("GetHelloName");

app.Run();


