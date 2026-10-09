using Microsoft.AspNetCore.Http.HttpResults;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Endpoints;

public static class TodoEndpoints
{
    public static RouteGroupBuilder MapTodoEndpoints(this IEndpointRouteBuilder app)
    {
        var todosGroup = app.MapGroup("/todos").WithTags("Todos");

        todosGroup.MapGet("/", Ok<TodoItem[]>(ITodoRepository repository ) =>
            {
                return TypedResults.Ok(repository.GetAll());
            })
            .WithName("GetTodos");

        todosGroup.MapGet("/{id}", Results<Ok<TodoItem>, NotFound<string>>(ITodoRepository repository, Guid id ) =>
            {
                var result = repository.GetById(id);

                return result == null ? TypedResults.NotFound("There is no item by id") : TypedResults.Ok(result);
            })
            .WithName("GetTodosById");

        todosGroup.MapPost("/", Results<CreatedAtRoute<TodoItem>, BadRequest<string>>(ITodoRepository repository, CreateTodoRequest todoItem ) =>
            {
                if(string.IsNullOrWhiteSpace(todoItem.Title))
                {
                    return TypedResults.BadRequest("Title cannot be null or empty");
                }

                var result = repository.Add(isDone: todoItem.IsDone, title: todoItem.Title);
        
                return TypedResults.CreatedAtRoute(result,"GetTodosById", new { id = result.Id });
            })
            .WithName("PostTodos");


        todosGroup.MapPut("/{id}",  Results<NoContent, NotFound<string>, BadRequest<string>>(ITodoRepository repository, CreateTodoRequest todoItem, Guid id ) =>
            {
                if(string.IsNullOrWhiteSpace(todoItem.Title))
                {
                    return TypedResults.BadRequest("Title cannot be null or empty");
                }
        
                var result = repository.Update(isDone: todoItem.IsDone, title: todoItem.Title, id);
        
                return result ? TypedResults.NoContent() : TypedResults.NotFound("There is no item by id");
            })
            .WithName("UpdateTodos");

        todosGroup.MapDelete("/{id}", Results<NoContent, NotFound<string>>(ITodoRepository repository, Guid id ) =>
            {
                var result = repository.Remove(id);
        
                return result ? TypedResults.NoContent() : TypedResults.NotFound("There is no item by id");
            })
            .WithName("DeleteTodos");
        
        return todosGroup;
    }
}