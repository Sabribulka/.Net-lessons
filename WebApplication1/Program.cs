using Microsoft.AspNetCore.Http.HttpResults;
using WebApplication1.Models;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<IWeatherService, WeatherService>();
builder.Services.AddSingleton<ITodoRepository, InMemoryTodoRepository>();
// builder.Services.AddScoped<IWeatherService, WeatherService>();
// builder.Services.AddTransient<IWeatherService, WeatherService>();
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


app.MapGet("/debug/ids",(IWeatherService weatherServiceFirst, IWeatherService weatherServiceSecond) =>
    {
        return new { FirstId = weatherServiceFirst.Id, SecondId = weatherServiceSecond.Id };
    })
    .WithName("GetDebugIds");

app.MapGet("/hello", () =>
    {
        return "Hello Anna!";
    })
    .WithName("GetHello");

app.MapGet("/hello/{name}", (string name) =>
        $"Hello {name}!")
    .WithName("GetHelloName");


// TODOS

app.MapGet("/todos", Ok<TodoItem[]>(ITodoRepository repository ) =>
    {
        return TypedResults.Ok(repository.GetAll());
    })
    .WithName("GetTodos");

app.MapGet("/todos/{id}", Results<Ok<TodoItem>, NotFound<string>>(ITodoRepository repository, Guid id ) =>
    {
        var result = repository.GetById(id);

        return result == null ? TypedResults.NotFound("There is no item by id") : TypedResults.Ok(result);
    })
    .WithName("GetTodosById");

app.MapPost("/todos", Results<Ok<TodoItem>, BadRequest<string>>(ITodoRepository repository, CreateTodoRequest todoItem ) =>
    {
        if(string.IsNullOrWhiteSpace(todoItem.Title))
        {
            return TypedResults.BadRequest("Title cannot be null or empty");
        }

        return TypedResults.Ok(repository.Add(isDone: todoItem.IsDone, title: todoItem.Title));
    })
    .WithName("PostTodos");


app.MapPut("/todos/{id}",  Results<NoContent, NotFound<string>, BadRequest<string>>(ITodoRepository repository, CreateTodoRequest todoItem, Guid id ) =>
    {
        if(string.IsNullOrWhiteSpace(todoItem.Title))
        {
            return TypedResults.BadRequest("Title cannot be null or empty");
        }
        
        var result = repository.Update(isDone: todoItem.IsDone, title: todoItem.Title, id);
        
        return result ? TypedResults.NoContent() : TypedResults.NotFound("There is no item by id");
    })
    .WithName("UpdateTodos");

app.MapDelete("/todos/{id}", Results<NoContent, NotFound<string>>(ITodoRepository repository, Guid id ) =>
    {
        var result = repository.Remove(id);
        
        return result ? TypedResults.NoContent() : TypedResults.NotFound("There is no item by id");
    })
    .WithName("DeleteTodos");

app.Run();


