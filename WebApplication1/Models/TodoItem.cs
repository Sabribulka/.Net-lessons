namespace WebApplication1.Models;

public record TodoItem
{
    public Guid Id { get; private set; }
    public string Title { get; set; }
    public bool IsDone { get; set; }
    public DateTime CreatedAt { get; private set; }
    
    public TodoItem(bool isDone, string title)
    {
        Id = Guid.NewGuid();
        Title = title;
        IsDone = isDone;
        CreatedAt = DateTime.UtcNow;
    }
}