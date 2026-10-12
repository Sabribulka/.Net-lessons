using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class EfTodoRepository : ITodoRepository
{
    public EfTodoRepository(TodoDbContext dbContext, ILogger<EfTodoRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    
    private readonly TodoDbContext _dbContext;
    private readonly ILogger _logger;
    
    public TodoItem[] GetAll()
    {
        return _dbContext.TodoItems.ToArray();
    }

    public TodoItem? GetById(Guid id)
    {
        return _dbContext.TodoItems.Find(id);
    }

    public TodoItem Add(bool isDone, string title)
    {
       var item = new TodoItem(isDone, title);
       
       _dbContext.TodoItems.Add(item);
       _dbContext.SaveChanges();
       
        _logger.LogInformation("Added new item {title} with id {id}", title, item.Id);
       
       return item;
    }
    
    public bool Update(bool isDone, string title, Guid id)
    {

        var item = _dbContext.TodoItems.Find(id);
       
        if (item == null)
        {
            return false;
        }

        item.IsDone = isDone;
        item.Title = title;
        _dbContext.SaveChanges();
        
        _logger.LogInformation("The item with id {id} was updated", id);
        
       
        return true;
    }
    
    public bool Remove(Guid id)
    {
        var item = _dbContext.TodoItems.Find(id);

        if (item == null)
        {
            return false;
        }
        _dbContext.TodoItems.Remove(item);
        _dbContext.SaveChanges();
        
        _logger.LogInformation("Removed item with id {id}", id);
        
       
        return true;
    }
}