using WebApplication1.Models;

namespace WebApplication1.Services;

public interface ITodoRepository
{
    TodoItem[] GetAll();
    TodoItem? GetById(Guid id);
    
    TodoItem Add(bool isDone, string title);
    bool Update(bool isDone, string title, Guid id);
    bool Remove(Guid id);
}