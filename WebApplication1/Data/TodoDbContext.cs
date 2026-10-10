using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> dbContextOptions): base(dbContextOptions)
    {
    }
    
    public DbSet<TodoItem> TodoItems { get; set; }
    
}