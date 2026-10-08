namespace WebApplication1.Models;

public record TodoItem
{
    public Guid Id { get; }
    public string Title { get; set; }
    public bool IsDone { get; set; }
    public DateTime CreatedAt { get; }
    
    public TodoItem(bool isDone, string title)
    {
        Id = Guid.NewGuid();
        Title = title;
        IsDone = isDone;
        CreatedAt = DateTime.UtcNow;
    }
}