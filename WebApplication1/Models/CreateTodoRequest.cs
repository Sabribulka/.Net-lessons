namespace WebApplication1.Models;

public class CreateTodoRequest
{
    public required string Title { get; set; }
    public bool IsDone { get; set; }
}