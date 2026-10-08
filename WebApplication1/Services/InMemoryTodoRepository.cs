using WebApplication1.Models;

namespace WebApplication1.Services;

public class InMemoryTodoRepository : ITodoRepository
{
    private readonly List<TodoItem> _todoList = [];
    
    public TodoItem[] GetAll()
    {
        return _todoList.ToArray();
    }
    
    public TodoItem? GetById(Guid id)
    {
        return _todoList.Find((item) => item.Id == id);
    }

    public TodoItem Add(bool isDone, string title)
    {
        var newItem = new TodoItem(isDone, title);
        
        _todoList.Add(newItem);

        return newItem;
    }
    
    public bool Update(bool isDone, string title, Guid id)
    {
        var updatedItem = _todoList.Find((item) => item.Id == id);
        
        if (updatedItem == null)
        {
            return false;
        }
        
       
        updatedItem.IsDone = isDone;
        updatedItem.Title = title;

        return true;
    }
    
    public bool Remove(Guid id)
    {
        var deletedItem = _todoList.Find((item) => item.Id == id);
        
        if (deletedItem == null)
        {
            return false;
        }
       
        _todoList.Remove(deletedItem);

        return true;
    }
}